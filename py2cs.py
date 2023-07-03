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

namespace Unidecode.NET
{
    public static partial class Unidecoder
    {
        private static readonly string[][] characters;
        private static readonly int MaxDecodedCharLength;

        static Unidecoder()
        {
            characters = new string[][]
            {
''')


def formatch(ch, cc):
    ch = ch.replace("\r", "")
    ch = ch.replace("\\", "\\\\")
    ch = ch.replace("\"", "\\\"")
    ch = ch.replace("\n", '"+Environment.NewLine+"')
    return ch if cc > 31 else "\\u" + ('%x' % cc).rjust(4, '0')

lastFoundIndex = -1
indent = '             '
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
        idx = int(m.group(1), 16)
        num = idx * 256
        fp.write(indent)
        for missingindex in range(lastFoundIndex+1,idx):
            fp.write('/* %3s */ null,\n' % (missingindex))
            fp.write(indent)
        lastFoundIndex = idx
        fp.write('/* %3s */  /*%5s %s*/ new[] {' % (idx, num, m.group(1)))
        for ch in data:
            if ch is None:
                fp.write('"%s" /*%s*/%s ' % (
                    '',
                    ("%x" % (num + c)).rjust(4, '0'),
                    "," if c < 255 else ""))
            else:
                fp.write('"%s" /*%s*/%s ' % (
                    formatch(ch, num + c),
                    ("%x" % (num + c)).rjust(4, '0'),
                    "," if c < 255 else ""))
                c = c + 1
        fp.write('},\n')

fp.write(
    '''         };
                MaxDecodedCharLength = 1;
                foreach (var block in characters)
                {
                    if (block == null)
                        continue;
                    foreach (var str in block)
                        if (str.Length > MaxDecodedCharLength)
                            MaxDecodedCharLength = str.Length;
                }
            }
        }
    }
    ''')
print("converted!")

