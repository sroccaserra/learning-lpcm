\ ---------------------------------------------------------------------------- \
\ Useful constants and words.
\ ---------------------------------------------------------------------------- \

pi f2* Fconstant tau

440e Fconstant a-440-freq

2e 8e f** 0.999e f* Fconstant max_8_bit_volume
2e 16e f** 0.999e f* Fconstant max_16_bit_volume

44100e Fconstant cd_sample_rate
cd_sample_rate 1/f Fconstant cd_sample_width

8000e Fconstant low_sample_rate
low_sample_rate 1/f Fconstant low_sample_width

\ low_sample_rate Fconstant sample_rate
\ low_sample_width Fconstant sample_width
cd_sample_rate Fconstant sample_rate
cd_sample_width Fconstant sample_width

: f2dup ( r1 r2 -- r1 r2 r1 r2 )
    fover fover ;

\ ---------------------------------------------------------------------------- \
\ Wave words.
\ These words generate float values between -1 and 1.
\ ---------------------------------------------------------------------------- \

: sine-wave ( t freq -- r )
    \ Generates a float value between -1 and 1
    tau f* f* fsin ;

: square-wave ( t freq -- r )
    \ Generates a float value between -1 and 1
    sine-wave f0> if
        1e
    else
        -1e
    then ;

: mixed-square-and-sine-wave ( t freq -- r )
    .15e .85e { f: square-k f: sine-k }
    f2dup square-wave square-k f*
    f-rot sine-wave sine-k f*
    f+ ;

\ ---------------------------------------------------------------------------- \
\ Output generation, one value at a time.
\ These words assume that r is a float value between -1 and 1.
\ ---------------------------------------------------------------------------- \

: u8-mono-out ( r -- )
    \ Assumes r is a float value between -1 and 1
    1e f+ f2/ max_8_bit_volume f*
    f>s emit ;

: s16_le-mono-out ( r -- )
    \ Assumes r is a float value between -1 and 1
    f2/ max_16_bit_volume f* f>s
    dup emit
    256 / emit ;

: s16_be-mono-out ( r -- )
    \ Assumes r is a float value between -1 and 1
    f2/ max_16_bit_volume f* f>s
    dup 256 / emit
    emit ;

\ ---------------------------------------------------------------------------- \
\ Main
\ ---------------------------------------------------------------------------- \

: tick-to-t ( n -- r )
    s>f sample_width f* ;

: main
    sample_rate f>s 0 do
        i tick-to-t a-440-freq

        \ sine-wave
        \ square-wave 8e f/
        mixed-square-and-sine-wave

        s16_le-mono-out
        \ u8-mono-out
    loop ;