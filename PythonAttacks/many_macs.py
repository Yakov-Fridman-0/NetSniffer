from scapy.all import sendp, RandMAC
from scapy.layers.l2 import Ether

if __name__ == "__main__":
    sendp(Ether(dst=RandMAC(), src=RandMAC()), count=200)
