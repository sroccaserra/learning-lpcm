Learning how to create LPCM encoded signals.

## Use

I'm starting to learn using Gforth and aplay (under Linux) or SoX.

```
$ gforth src/lpcm.fs -e 'main bye' | aplay -t raw -c 1 -f s16_le -r 44100
```

To use with SoX (Windows, macOS, Linux):

```
$ gforth src/lpcm.fs -e 'main bye' | play -t raw -c 1 -e signed-integer -b 16 -L -r 44100 -
```

To change the sample rate, you can put a float on the float stack before starting the program (it should match the rate passed to aplay / SoX):

```
$ gforth -e '8000e' src/lpcm.fs -e 'main bye' | aplay -t raw -c 1 -f s16_le -r 8000
```

To change the sample size, you can put a 8 (for `u8`) or a 16 (for `s16_le`) on the stack before starting the program (it should match the bits passed to aplay / SoX):

```
$ gforth -e '8' src/lpcm.fs -e 'main bye' | aplay -t raw -c 1 -f u8 -r 44100
```

## Run the tests

```
$ gforth src/lpcm-test.fs -e bye
```

## References

- Pulse-code modulation ~ <https://en.wikipedia.org/wiki/Pulse-code_modulation>
- PCM ~ <https://wiki.multimedia.cx/index.php/PCM>
- Gforth ~ <https://gforth.org/manual/>
- Floating Point ~ <https://gforth.org/manual/Floating-Point.html>
- aplay ~ <https://alsa.opensrc.org/Aplay>
- SoX ~ <http://sox.sourceforge.net/>
- Envelope (music) ~ <https://en.wikipedia.org/wiki/Envelope_(music)>
