[![NuGet](https://img.shields.io/nuget/v/Intervals.svg?maxAge=2592000)](https://www.nuget.org/packages/Intervals/)
 [![license](https://img.shields.io/github/license/unterrainerinformatik/Intervals.svg?maxAge=2592000)](http://unlicense.org)  [![Twitter Follow](https://img.shields.io/twitter/follow/throbax.svg?style=social&label=Follow&maxAge=2592000)](https://twitter.com/throbax)  

# General

This section contains various useful projects that should help your development-process.  

This section of our GIT repositories is free. You may copy, use or rewrite every single one of its contained projects to your hearts content.  
In order to get help with basic GIT commands you may try [the GIT cheat-sheet][coding] on our [homepage][homepage].  

This repository located on our  [homepage][homepage] is private since this is the master- and release-branch. You may clone it, but it will be read-only.  
If you want to contribute to our repository (push, open pull requests), please use the copy on github located here: [the public github repository][github]  

# ![Icon](https://github.com/UnterrainerInformatik/Intervals/raw/master/icon.png) Faders

This is a PCL library that contains helper classes that should make handling intervals and faders (sliders) more comfortable. 
So if you want some value clipped between two distinct values, some nice constructors for that and maybe tell the structure to advance the value to 33% and read the resulting value again, this is for you.   

> **If you like this repo, please don't forget to star it.**
> **Thank you.**



## Getting Started

#### Examples

##### Interval

Instead of two variables, you can now write intervals in a shorter form.

```c#
InitialSpeed = new Interval<float>(0, 0);
Acceleration = new Interval<float>(0, 0);
// Min exclusive, max inclusive.
LifeTime = new Interval<float>(.01f, .05f, true, false);
// Min exclusive, max exclusive.
Scale = new Interval<float>(0.05f, 0.25f, true, true);
NumberOfParticlesToEmit = new Interval<int>(1, 3);

// Would return false.
Scale.IsInBetween(0f);
// Would return true.
Scale.IsInBetween(0.051f);

```

##### Fader

Faders (as in sliders) are even cooler.
You can advance them and they are guarantied to always stay in bounds.
And they have a percentage-field that you may query, or even set and then query the value-field since the two of them are always kept in sync.

```C#
Fader f = new Fader(0f, 500f);

```



[homepage]: http://www.unterrainer.info
[coding]: http://www.unterrainer.info/Home/Coding
[github]: https://github.com/UnterrainerInformatik/BloomEffectRenderer