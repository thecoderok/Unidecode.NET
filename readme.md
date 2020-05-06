# Unidecode.NET

[![Build status](https://ci.appveyor.com/api/projects/status/lqjnq9ve1vbv393u?svg=true)](https://ci.appveyor.com/project/phnx47/unidecode-net)
[![NuGet](https://img.shields.io/nuget/v/Unidecode.NET.svg)](https://www.nuget.org/packages/Unidecode.NET)
[![NuGet](https://img.shields.io/nuget/dt/Unidecode.NET.svg)](https://www.nuget.org/packages/Unidecode.NET)
[![License MIT](https://img.shields.io/badge/license-MIT-green.svg)](https://opensource.org/licenses/MIT) 

Support <img src="https://img.shields.io/badge/.net-4.5-green.svg"></img>, <img src="https://img.shields.io/badge/.netstandard-1.2-green.svg"></img>, <img src="https://img.shields.io/badge/.netstandard-2.0-green.svg"></img>  

# Purpose
It often happens that you have text data in Unicode, but you need to represent it in ASCII. For example when integrating with legacy code that doesn’t support Unicode, or for ease of entry of non-Roman names on a US keyboard, or when constructing ASCII machine identifiers from human-readable Unicode strings that should still be somewhat intelligible (a popular example of this is when making an URL slug from an article title).

# Overview

**Unidecode.NET** is .NET library dll, written in C#.
It provides string or char extension method Unidecode() that returns transliterated string. It supports huge amount of languages.
And it's very easy to add your language if it's not supported already!

# Installation
Unidecode.NET published as NuGet package: https://www.nuget.org/packages/Unidecode.NET/

To install Unidecode.NET, run the following command in the [Package Manager Console](https://docs.nuget.org/consume/package-manager-console)

`Install-Package Unidecode.NET`

#Example code

Take a look at the list of assertions:
```cs
Assert.AreEqual("Bei Jing ", "\u5317\u4EB0".Unidecode());
Assert.AreEqual("Rabota s kirillitsey", "Работа с кириллицей".Unidecode());
Assert.AreEqual("aeoeuoAeOeUO", "äöűőÄÖŨŐ".Unidecode());
Assert.AreEqual("Hello, World!", "Hello, World!".Unidecode());
Assert.AreEqual("'\"\r\n", "'\"\r\n".Unidecode());
Assert.AreEqual("CZSczs", "ČŽŠčžš".Unidecode());
Assert.AreEqual("a", "ア".Unidecode());
Assert.AreEqual("a", "α".Unidecode());
Assert.AreEqual("a", "а".Unidecode());
Assert.AreEqual("chateau", "ch\u00e2teau".Unidecode());
Assert.AreEqual("vinedos", "vi\u00f1edos".Unidecode());
```
Other implementations
---------------------

*  [Text::Unidecode for Perl](http://search.cpan.org/~sburke/Text-Unidecode/lib/Text/Unidecode.pm) (the original implementation)
*  [Unidecode for Python](https://pypi.python.org/pypi/Unidecode)
*  [unidecoder for Ruby](https://github.com/norman/unidecoder)
*  [unidecode for JavaScript](https://github.com/FGRibreau/node-unidecode)


Credits
-------

This project is a fork of the [unidecodesharpfork](https://bitbucket.org/DimaStefantsov/unidecodesharpfork) written by Dima Stefantsov.


License
-------

This project is licensed under [MIT](https://opensource.org/licenses/MIT).

Character transliteration tables used in this project are converted (and slightly modified) from the tables provided in
the Perl library [Text::Unidecode] by Sean M. Burke and are distributed under the Perl license.


[Text::Unidecode]: http://search.cpan.org/~sburke/Text-Unidecode/lib/Text/Unidecode.pm
