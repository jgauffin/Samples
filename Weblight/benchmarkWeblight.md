Benchmarking Weblight
============================

This test is to check how fast Weblight is.


# download apache

http://www.apachehaus.com/cgi-bin/download.plx

No need to install it. You just need the benchmarking tool `bin\ab.exe`.

# Compile weblight.

1. Open the solution `WebLight.sln`
2. Compile it as `Release`
3. Close VStudio
4. Start `Weblight.Console\bin\Release\Weblight.Console.exe`

# Run benchmarking

```
ab -n 10000 -c 50 -k http://localhost/emptyaspnet/
```

Make sure that `-n` and `c` is the same of my server and ASP.NET
