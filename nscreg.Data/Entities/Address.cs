﻿using nscreg.Utilities.Attributes;
using nscreg.Utilities.Enums;

namespace nscreg.Data.Entities
{
    public class Address
    {
        public int Id { get; set; }  //	ID generated by the system
        public string AddressPart1 { get; set; }    //	First line of the address as used in the country (for instance street name and number)
        public string AddressPart2 { get; set; }    //	Second line of address (for instance city and zip code)
        public string AddressPart3 { get; set; }    //	
        public string AddressPart4 { get; set; }    //	
        public string AddressPart5 { get; set; }    //	(Kyrgyzstan has 5 lines of address)
        public virtual Region Region { get; set; }
        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public int RegionId { get; set; }
        public string GpsCoordinates { get; set; }
    }
}
