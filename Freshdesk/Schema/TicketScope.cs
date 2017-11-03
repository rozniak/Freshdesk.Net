/*
 * Freshdesk.Schema.TicketScope -- Freshdesk Agent Ticket Permissions Scope Enumeration
 *
 * This source-code is part of the Freshdesk API for C# library by Rory Fewell (rozniak) of Oddmatics for Agile ICT for Education Ltd.:
 * <<https://oddmatics.uk>>
 * <<http://www.agileict.co.uk>>
 * 	
 * Copyright (C) 2017 Oddmatics
 * 	
 * Sharing, editing and general licence term information can be found inside of the "LICENSE.MD" file that should be located in the root of this project's directory structure.
 */

namespace Freshdesk.Schema
{
    /// <summary>
    /// Specifies constants to determine the level of access an agent has to tickets.
    /// </summary>
    public enum TicketScope
    {
        GlobalAccess = 1,
        GroupAccess = 2,
        RestrictedAccess = 3
    }
}
