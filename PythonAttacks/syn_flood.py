from scapy.all import sendp, RandIP, RandShort
from scapy.layers.l2 import Ether
from scapy.layers.inet import IP, TCP

if __name__ == "__main__":
    targetIP = "10.100.102.201"
    targetPort = 443

    wifi = "Realtek RTL8852AE WiFi 6 802.11ax PCIe Adapter"
    eth = "Realtek Gaming GbE Family Controller"

    source_mac = "00:00:00:00:00:02"
    target_mac = "00:00:00:00:00:01"

    sendp(Ether(src=source_mac, dst=target_mac) /
          IP(src=RandIP(), dst=targetIP) /
          TCP(dport=targetPort, sport=RandShort(), flags="S"),
          iface=eth, count=20)
