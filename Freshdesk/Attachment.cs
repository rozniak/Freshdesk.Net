﻿/*
 * Copyright 2015 Beckersoft, Inc.
 *
 * Author(s):
 *  Oleg Shevchenko (shevchenko.oleg@outlook.com)
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

using System.IO;

namespace Freshdesk
{
    public class Attachment
    {
        public Stream Content { get; set; }
        public string FileName { get; set; }
    }
}
