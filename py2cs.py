import os, re

d = "py-codes" # https://github.com/avian2/unidecode/tree/master/unidecode
print("working...")

fp = open("Unidecoder.Characters.cs", "w")
fp.write('''/*
COPYRIGHT

Character transliteration tables:

Copyright 2001, Sean M. Burke <sburke@cpan.org>, all rights reserved.

Python code:

Copyright 2009, Tomaz Solc <tomaz@zemanta.com>

The programs and documentation in this dist are distributed in the
hope that they will be useful, but without any warranty; without even
the implied warranty of merchantability or fitness for a particular
purpose.

This library is free software; you can redistribute it and/or modify
it under the same terms as Perl.
*/

/*
Don't edit, this code is generated.
*/

using System;
using System.Collections.Generic;

namespace Unidecode.NET 
{
    public static partial class Unidecoder
    {
        private static readonly Dictionary<int, string[]> characters;

        static Unidecoder()
        {
            characters = new Dictionary<int, string[]> {
''')


def formatch(ch, cc):
    ch = ch.replace("\r", "")
    ch = ch.replace("\\", "\\\\")
    ch = ch.replace("\"", "\\\"")
    ch = ch.replace("\n", '"+Environment.NewLine+"')
    return ch if cc > 31 else "\\u" + ('%x' % cc).rjust(4, '0')


for file in [file for file in os.listdir(d) if not file in [".", ".."]]:
    m = re.search('x(.{3})\.py$', file)
    if m:
        data = __import__(d + "." + file[0:-3], [], [], ['data']).data
        missing = 256 - len(data)
        if missing != 0:
            fill = "[?]" if data[-1] == "[?]" else ""
            data += (fill,)*missing
        assert len(data) == 256
        c = 0
        num = int(m.group(1), 16) * 256
        fp.write('                {%s /*%s %s*/, new[]{\n' % (int(m.group(1), 16), num, m.group(1)))
        for ch in data:
            fp.write('"%s" /*%s*/%s ' % (
                formatch(ch, num + c),
                ("%x" % (num + c)).rjust(4, '0'),
                "," if c < 255 else ""))
            c = c + 1
        fp.write('}},\n\n')

fp.write(
    '''            };
            }
        }
    }
    ''')
print("converted!")

