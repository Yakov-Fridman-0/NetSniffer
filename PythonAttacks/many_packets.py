from scapy.all import send, sendp, RandMAC, RandIP, RandShort
from scapy.layers.l2 import Ether
from scapy.layers.inet import IP, UDP, TCP

if __name__ == "__main__":
    sendp(Ether(dst=RandMAC(), src=RandMAC()), count=1000)
    send(IP(dst=RandIP(), src=RandIP())/TCP(dport=RandShort(), sport=RandShort(), flags=), count=1000)
    send(IP(dst=RandIP(), src=RandIP())/UDP(dport=RandShort(), sport=RandShort()), count=1000)
