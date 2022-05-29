using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using PcapDotNet.Packets;


namespace NetSnifferLib
{
    public class DisplayFilter
    {
        static readonly Dictionary<string, DisplayFilter> filters = new();

        const string emptyCharachters = @"";

        delegate bool PacketDataCondition(PacketData packetData);

        private PacketDataCondition Condition { get; init; } = new(packetData => true);

        public static DisplayFilter EmptyFilter { get; } = new(new PacketDataCondition(PacketData => true));

        private DisplayFilter(PacketDataCondition condition)
        {
            Condition = condition;
        }

        public static bool TryParse(string filterString, ref DisplayFilter filter)
        {
            if (filterString == null)
                return false;

            var cleanedFilter = Clean(filterString);

            if (cleanedFilter == string.Empty)
            {
                filter = EmptyFilter;
                return true;
            }
            else if (filters.ContainsKey(filterString))
            {
                filter = filters[filterString];
                return true;
            }
            else
            {
                PacketDataCondition condition = null;

                if (TryGetCondition(cleanedFilter, ref condition))
                {
                    filter = new DisplayFilter(condition);
                    filters.Add(filterString, filter);
                    return true;
                }

                return false;
            }
        }
        static string Clean(string filter)
        {
            filter = filter.Trim().ToLower();
            return Regex.Replace(filter, string.Empty, emptyCharachters, RegexOptions.Compiled);
        }

        readonly static string[] logicalOperatotrs = { "||", "&&" };

        readonly static Dictionary<string, ValuesComparer> comparisonOperators = new()
        {
            { "==", VariableEquals },
            { "!=", VariableNotEquals }
        };

        static bool TryGetCondition(string filterString, ref PacketDataCondition condition)
        {
            // multi-coditioanl
            if (logicalOperatotrs.Any(@operator => filterString.Contains(@operator)))
            {
                Match match;

                string leftCondition = string.Empty, @operator = string.Empty, rightCondition = string.Empty;

                if (!filterString.Contains("(", StringComparison.InvariantCulture) && !filterString.Contains(")", StringComparison.InvariantCulture))
                {
                    match = Regex.Match(filterString, @"^(?<leftCondition>)(?<operator>&&|\|\|)(?<rightCondition>)$");

                    if (!match.Success)
                        return false;

                    leftCondition = Clean(match.Groups["leftCondition"].Value);
                    @operator = match.Groups["operator"].Value;
                    rightCondition = Clean(match.Groups["rightCondition"].Value);
                }
                else
                {
                    try
                    {
                        match = Regex.Match(filterString, @"^(?<leftCondition>>\((?<c>)|[^()]+|\)(?<-c>))*(?(c)(?!))(?<operator>&&|\|\|)(?<rightCondition>>(?<c>)|[^()]+|\)(?<-c>))*(?(c)(?!))$", RegexOptions.None, TimeSpan.FromSeconds(1));
                    }
                    catch (TimeoutException)
                    {
                        return false;
                    }

                    if (match.Success && match.Groups.Cast<Group>().Where(group => !group.Value.Equals("")).ToList().Count == 4)
                    {
                        leftCondition = Clean(match.Groups["leftCondition"].Value);
                        @operator = match.Groups["operator"].Value;
                        rightCondition = Clean(match.Groups["rightCondition"].Value);
                    }
                    else
                    {
                        match = Regex.Match(filterString, @"(?<condition>\((?>\((?<c>)|[^()]+|\)(?<-c>))*(?(c)(?!))\))", RegexOptions.None, TimeSpan.FromSeconds(0.5));

                        if (!match.Success)
                            return false;

                        var leftConditionRaw = match.Groups["condition"].Value;
                        leftCondition = Clean(UnfoldPatamthesis(leftConditionRaw));

                        filterString = filterString.Remove(filterString.IndexOf(leftConditionRaw), leftConditionRaw.Length);

                        var nextMatch = match.NextMatch();
                        if (nextMatch.Success)
                        {
                            var rightConditionRaw = nextMatch.Captures[0].Value;
                            rightCondition = UnfoldPatamthesis(Clean(rightConditionRaw));

                            filterString = filterString.Remove(filterString.IndexOf(rightConditionRaw), rightConditionRaw.Length);
                        }
                        else
                        {
                            match = Regex.Match(filterString.Trim('&', '|', ' '), @"(?<condition>(?>\((?<c>)|[^()]+|\)(?<-c>))*(?(c)(?!)))", RegexOptions.None, TimeSpan.FromSeconds(0.5));

                            if (!match.Success)
                                return false;

                            rightCondition = match.Captures[0].Value;

                            filterString = filterString.Remove(filterString.IndexOf(rightCondition), rightCondition.Length);
                        }

                        @operator = filterString.Trim();

                        if (@operator is not "&&" and not "||")
                            return false;
                    }
                }


                PacketDataCondition condition1 = null, condition2 = null;

                if (TryGetCondition(leftCondition, ref condition1) && TryGetCondition(rightCondition, ref condition2))
                {
                    switch (@operator)
                    {
                        case "&&":
                            condition = new PacketDataCondition(packetData => condition1(packetData) && condition2(packetData));
                            break;
                        case "||":
                            condition = new PacketDataCondition(packetData => condition1(packetData) || condition2(packetData));
                            break;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            //single-conditioanl
            else
            {
                // has operators
                if (comparisonOperators.Keys.Any(@operator => filterString.Contains(@operator)))
                {
                    filterString = Regex.Replace(filterString, @"\s+", "");

                    var match  = Regex.Match(filterString, @"^(?<variable>[^=!]+)(?<operator>==|!=)(?<literal>[^=!]+)$");
                    if (match.Groups.Count != 4)
                        return false;

                    var variable = match.Groups["variable"].Value;
                    var @operator = match.Groups["operator"].Value;
                    var literal = match.Groups["literal"].Value;

                    var matchVariable  = Regex.Match(variable, @"(?<protocol>[^\.]+)\.(?<field>[^\.]+)");
                    if (matchVariable.Groups.Count != 3)
                        return false;

                    var protocol = matchVariable.Groups["protocol"].Value;
                    var field = matchVariable.Groups["field"].Value;

                    object value = null;
                    if (ProtocolNameComparer.IsValidProtocol(protocol) && PacketData.IsValidField(protocol, field) && PacketData.TryConvert(protocol, field, literal, ref value))
                    {
                        condition = new PacketDataCondition(packetData => PacketDataContainsProtocol(protocol, packetData) && comparisonOperators[@operator](packetData.GetField(protocol, field), value));
                        return true;
                    }

                    return false;
                }
                
                // has no operators
                else
                {
                    string protocol = UnfoldPatamthesis(filterString);

                    if (ProtocolNameComparer.IsValidProtocol(protocol))
                    {
                        condition = new PacketDataCondition(packetData => PacketDataContainsProtocol(protocol, packetData));
                        return true;
                    }

                    return false;
                }
            }
        }

        public bool PacketMatches(PacketData packetData)
        {
            return Condition(packetData);
        }

        public bool PacketMatches(Packet packet)
        {
            int packetId = IdManager.GetId(packet);
            var pakcetData = PacketData.GetPacketDataByPacketId(packetId);

            return PacketMatches(pakcetData);
        }

        static bool PacketDataContainsProtocol(string protocol, PacketData packetData)
        {
            return packetData[protocol] != null;
        }

        delegate bool ValuesComparer(object variable, object value);

        static bool VariableEquals(object variable, object value)
        {
            var result = variable.Equals(value);
            return result;
        }

        static string UnfoldPatamthesis(string text)
        {
            if (text.Length == 0)
                return text;

            if (text[0] == '(' && text[^1] == ')')
            {
                text = text[1..^1];
            }

            return text;
        }

        static bool VariableNotEquals(object variable, object value)
        {
            return !VariableEquals(variable, value);
        }
    }
}
