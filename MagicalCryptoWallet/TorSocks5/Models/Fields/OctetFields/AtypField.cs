﻿using MagicalCryptoWallet.Bases;
using MagicalCryptoWallet.Helpers;
using System.Net;
using System.Net.Sockets;

namespace MagicalCryptoWallet.TorSocks5.Models.Fields.OctetFields
{
	public class AtypField : OctetSerializableBase
	{
		#region Statics

		// https://gitweb.torproject.org/torspec.git/tree/socks-extensions.txt
		// IPv6 is not supported in CONNECT commands.

		public static AtypField IPv4
		{
			get
			{
				var atyp = new AtypField();
				atyp.FromHex("01");
				return atyp;
			}
		}

		public static AtypField DomainName
		{
			get
			{
				var atyp = new AtypField();
				atyp.FromHex("03");
				return atyp;
			}
		}

		#endregion

		#region ConstructorsAndInitializers

		public AtypField()
		{

		}

		#endregion

		#region Serialization

		public void FromDstAddr(string dstAddr)
		{
			dstAddr = Guard.NotNullOrEmptyOrWhitespace(nameof(dstAddr), dstAddr, true);

			if (IPAddress.TryParse(dstAddr, out IPAddress address))
			{
				Guard.Same($"{nameof(address)}.{nameof(address.AddressFamily)}", AddressFamily.InterNetwork, address.AddressFamily);

				ByteValue = IPv4.ToByte();
			}
			else
			{
				ByteValue = DomainName.ToByte();
			}
		}

		#endregion
	}
}
