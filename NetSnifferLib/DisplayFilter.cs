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

        private PacketDataCondition Condition { get; init; }

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
                var match = Regex.Match(filterString, @"");

                return false;
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
                    string protocol = filterString;

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

        static bool VariableNotEquals(object variable, object value)
        {
            return !VariableEquals(variable, value);
        }
    }
}
