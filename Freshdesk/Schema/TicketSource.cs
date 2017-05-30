/*
 * Freshdesk.Schema.TicketSource -- Freshdesk Ticket Source Enumeration
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
    /// Specifies constants determining the source of a ticket.
    /// </summary>
    public enum TicketSource
    {
        Email,
        Portal,
        Phone,
        Forum,
        Twitter,
        Facebook,
        Chat,
        MobiHelp,
        FeedbackWidget,
        OutboundEmail,
        Ecommerce
    }
}
