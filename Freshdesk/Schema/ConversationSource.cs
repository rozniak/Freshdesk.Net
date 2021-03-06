﻿/*
 * Copyright 2015 Beckersoft, Inc.
 *
 * Author(s):
 *  Rory Fewell (rory.fewell@agileict.co.uk)
 *  
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
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
