[![NuGet](https://img.shields.io/nuget/v/Faders.svg?maxAge=2592000)](https://www.nuget.org/packages/Faders/) [![NuGet](https://img.shields.io/nuget/dt/Faders.svg?maxAge=2592000)](https://www.nuget.org/packages/Faders/)
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
Fader f = new Fader(0f, 100f){value = 10};
Assert.AreEqual(f.Value, 10, EPSILON);
Assert.AreEqual(f.Percentage, 10, EPSILON);
```

It can do the following:

* If you swap min and max in the constructor, those are switched back.
* If you set the value outside the interval, the value is capped (constrained to the interval) automatically.
* You can set or get the value using the Value property or the Percentage property. Those two correspond. Setting one corrects the other.
* The boundaries are mutable after construction of the fader.
* If you happen to cut your value while making the interval smaller, then the value (and the percentage) are corrected to point to the outer limits of the interval automatically.
* You can invert the fader by setting the IsInverted property and reverse the direction while still adding values... Imagine you want to lerp from 0 to 50 and back to 0 again. You can now add 1 every update and add an if that will invert the fader if value > 50. Viola.

###### ValueChangedEvent

You may register a ValueChangedEvent to get notified if the value of that fader changes.

```c#
[Test]
public void ValueChangedEventIsThrownCorrectly()
{
  double oldValue = 0;
  var f = new Fader(0, 100);
  f.ValueChanged += (sender, args) => { oldValue = args.OldValue; };

  f.Value = 100;
  Assert.AreEqual(oldValue, 0, EPSILON);
  f.Value = 10;
  Assert.AreEqual(oldValue, 100, EPSILON);
  f.Value = 100;
  Assert.AreEqual(oldValue, 10, EPSILON);
}
```



###### Tweening

Faders can be used to do tweening. Therefore they provide accessors to get the value after some common formulas like so:

```c#
[Test]
public void QuadraticValueFadersWork()
{
  var f = new Fader(20.0, 30.0) {Value = 25.0};
  Assert.IsTrue(f.QuadraticValue.Equals(22.5));
  Assert.AreEqual(f.QuadraticValue, 22.5, EPSILON);

  f.QuadraticValue = 25.0;
  Assert.AreEqual(f.Value, 27.0710678118, EPSILON);
}

[Test]
public void CubicValueFadersWork()
{
  var f = new Fader(20.0, 30.0) {Value = 25.0};
  Assert.AreEqual(f.CubicValue, 21.25, EPSILON);

  f.CubicValue = 25.0;
  Assert.AreEqual(f.Value, 27.9370052598, EPSILON);
}

[Test]
public void ExponentialValueFadersWork()
{
  var f = new Fader(20.0, 30.0) {Value = 25.0};
  Assert.AreEqual(f.ExponentialValue, 21.7360679775, EPSILON);

  f.ExponentialValue = 25;
  Assert.AreEqual(f.Value, 28.0043710646, EPSILON);
}

[Test]
public void SlowStartValueFadersWork()
{
  var f = new Fader(20.0, 30.0) {Value = 21.0};
  Assert.AreEqual(f.BidirectionalSlow, 20.24471741, EPSILON);

  f.BidirectionalSlow = 28.0;
  Assert.AreEqual(f.Value, 27.0483276469, EPSILON);
}

[Test]
public void QuickStartValueFadersWork()
{
  var f = new Fader(20.0, 30.0) {Value = 21.0};
  Assert.AreEqual(f.BidirectionalQuick, 22.44, EPSILON);

  f.BidirectionalQuick = 28.0;
  Assert.AreEqual(f.Value, 29.2171633333, EPSILON);
}
```



[homepage]: http://www.unterrainer.info
[coding]: http://www.unterrainer.info/Home/Coding
[github]: https://github.com/UnterrainerInformatik/BloomEffectRenderer
