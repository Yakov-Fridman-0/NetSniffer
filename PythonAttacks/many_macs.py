from scapy.all import sendp, RandMAC
from scapy.layers.l2 import Ether

from interfaces import interface

if __name__ == "__main__":
    sendp(Ether(dst=RandMAC(), src=RandMAC()), count=200, iface=interface)
