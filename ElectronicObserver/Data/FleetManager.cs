﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicObserver.Data {

	public class FleetManager : APIWrapper {
	
		public IDDictionary<FleetData> Fleets { get; private set; }


		public int CombinedFlag { get; internal set; }


		public FleetManager() {
			Fleets = new IDDictionary<FleetData>();
		}


		
		public override void LoadFromResponse( string apiname, dynamic data ) {
			base.LoadFromResponse( apiname, (object)data );

			//api_port/port
			foreach ( var elem in data ) {

				int id = (int)elem.api_id;

				if ( !Fleets.ContainsKey( id ) ) {
					var a = new FleetData();
					a.LoadFromResponse( apiname, elem );
					Fleets.Add( a );

				} else {
					Fleets[id].LoadFromResponse( apiname, elem );
				}
			}

		}



		public override void LoadFromRequest( string apiname, Dictionary<string, string> data ) {
			base.LoadFromRequest( apiname, data );

			switch ( apiname ) {
				case "api_req_hensei/change":
					Fleets[int.Parse( data["api_id"] )].LoadFromRequest( apiname, data );
					break;

				case "api_req_kousyou/destroyship":
					foreach ( int i in Fleets.Keys )
						Fleets[i].LoadFromRequest( apiname, data );
					break;

			}

		}
	}

}
