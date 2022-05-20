from scapy.all import send, sendp, RandMAC, RandIP, RandShort
from scapy.layers.l2 import Ether
from scapy.layers.inet import IP, UDP, TCP


src_mac = "00:00:00:00:00:01"
dst_mac = "00:00:00:00:00:02"

if __name__ == "__main__":
    sendp(Ether(dst=dst_mac, src=src_mac), loop=1, realtime=True, verbose=False)
    # send(IP(dst=RandIP(), src=RandIP())/TCP(dport=RandShort(), sport=RandShort()), count=2000, inter=0.2)
    # send(IP(dst=RandIP(), src=RandIP())/UDP(dport=RandShort(), sport=RandShort()), count=500, inter=0.2)
