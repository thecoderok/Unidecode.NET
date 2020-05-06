# Unidecode.NET

[![Build status](https://ci.appveyor.com/api/projects/status/lqjnq9ve1vbv393u?svg=true)](https://ci.appveyor.com/project/phnx47/unidecode-net)
[![NuGet](https://img.shields.io/nuget/v/Unidecode.NET.svg)](https://www.nuget.org/packages/Unidecode.NET)
[![NuGet](https://img.shields.io/nuget/dt/Unidecode.NET.svg)](https://www.nuget.org/packages/Unidecode.NET)
[![License MIT](https://img.shields.io/badge/license-MIT-green.svg)](https://opensource.org/licenses/MIT) 

# Purpose
It often happens that you have text data in Unicode, but you need to represent it in ASCII. 
For example when integrating with legacy code that doesn’t support Unicode, or for ease of entry of non-Roman names on a US keyboard, 
or when constructing ASCII machine identifiers from human-readable Unicode strings that should still be somewhat intelligible 
(a popular example of this is when making an URL slug from an article title).

# Overview

Unidecode is meant to be a transliterator of last resort, to be used once you've decided that you can't just display the Unicode data as is,
and once you've decided you don't have a more clever, language-specific transliterator available
or once you've already applied a smarter algorithm and now just want Unidecode to do cleanup.

In other words, when you don't like what Unidecode does, do it yourself. Really, that's what the above says. Here's how you would do this for German, for example:

In German, there's the typographical convention that an umlaut (the double-dots on: ä ö ü) can be written as an "-e", like with "Schön" becoming "Schoen". 
But Unidecode doesn't do that. Unidecode simply drop the umlaut accent and give back "Schon".

[More information](https://metacpan.org/pod/distribution/Text-Unidecode/lib/Text/Unidecode.pm)

# Example code
Take a look at the list of assertions:
```c#
Assert.Equal("Rabota s kirillitsei", "Работа с кириллицей".Unidecode());
Assert.Equal("aouoAOUO", "äöűőÄÖŨŐ".Unidecode());
Assert.Equal("Hello, World!", "Hello, World!".Unidecode());
Assert.Equal("'\"\r\n", "'\"\r\n".Unidecode());
Assert.Equal("CZSczs", "ČŽŠčžš".Unidecode());
Assert.Equal("a", "ア".Unidecode());
Assert.Equal("a", "α".Unidecode());
Assert.Equal("a", "а".Unidecode());
Assert.Equal("chateau", "ch\u00e2teau".Unidecode());
Assert.Equal("vinedos", "vi\u00f1edos".Unidecode());
```
Other implementations
---------------------

*  [Text::Unidecode for Perl](http://search.cpan.org/~sburke/Text-Unidecode/lib/Text/Unidecode.pm) (the original implementation)
*  [Unidecode for Python](https://pypi.python.org/pypi/Unidecode)

Credits
-------

This project is a fork of the [unidecodesharpfork](https://bitbucket.org/DimaStefantsov/unidecodesharpfork) written by Dima Stefantsov.


License
-------

This project is licensed under [MIT](https://opensource.org/licenses/MIT).

Character transliteration tables used in this project are converted from the tables provided in
[Unidecode for Python](https://github.com/avian2/unidecode) under the GPL-2.0 license which is port of 
the Perl library [Text::Unidecode] by Sean M. Burke and are distributed under the Perl license.

[Text::Unidecode]: http://search.cpan.org/~sburke/Text-Unidecode/lib/Text/Unidecode.pm
