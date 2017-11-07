/*
 * Freshdesk.Schema.ConversationSource -- Freshdesk Conversation Source Enumeration
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
    public enum ConversationSource
    {
        Reply = 0,
        Note = 2,
        CreatedViaTweets = 5,
        CreatedViaSurvey = 6,
        CreatedViaFacebook = 7,
        CreatedViaEmail = 8,
        CreatedViaPhone = 9,
        CreatedViaMobihelp = 10,
        Ecommerce = 11
    }
}
