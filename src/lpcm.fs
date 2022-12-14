\ ---------------------------------------------------------------------------- \
\ Useful constants and words.
\ ---------------------------------------------------------------------------- \

pi f2* Fconstant tau

440e Fconstant a-440-freq

: _f2dup ( r1 r2 -- r1 r2 r1 r2 )
    fover fover ;

\ ---------------------------------------------------------------------------- \
\ Voice state
\ ---------------------------------------------------------------------------- \

.1e Fconstant a
1e Fconstant d
.0e Fconstant s
.0e Fconstant r
false Constant is-hold

: attack ( t -- r )
    a f/ ;

: decay ( t -- r )
    d f0= if
        fdrop s exit
    then
    [ s 1e f- d f/ ] fliteral f*
    [ 1e s 1e f- a f* d f/ f- ] fliteral f+ ;

: sustain ( t -- r )
    fdrop s ;

: release ( t -- r )
    fdrop 0e ;

: adsr ( t -- r )
    case
        fdup a f<= ?of
            attack endof
        fdup a d f+ f<= ?of
            decay endof
        is-hold ?of
            sustain endof
        true ?of
            release endof
    endcase ;

\ ---------------------------------------------------------------------------- \
\ Wave words.
\ These words generate float values between -1 and 1.
\ ---------------------------------------------------------------------------- \

: sine-wave ( t freq -- r )
    tau f* f* fsin ;

: square-wave ( t freq -- r )
    sine-wave f0> if
        1e
    else
        -1e
    then ;

: saw-wave ( t freq -- r )
    f* f2*
    begin fdup 1e f> while
        2e f- repeat ;

: mixed-square-and-sine-wave ( t freq -- r )
    .15e .85e { f: square-k f: sine-k }
    _f2dup square-wave square-k f*
    f-rot sine-wave sine-k f*
    f+ ;

: sample-value ( t -- r )
    fdup adsr fswap ( envelope t )
    a-440-freq ( envelope t freq )

    \ sine-wave
    \ saw-wave
    square-wave 8e f/
    \ mixed-square-and-sine-wave
    f* ;

\ ---------------------------------------------------------------------------- \
\ Output generation, one value at a time.
\ These words assume that r is a float value between -1 and 1.
\ ---------------------------------------------------------------------------- \

2e 8e f** 0.999e f* Fconstant max_8_bit_volume
2e 16e f** 0.999e f* Fconstant max_16_bit_volume

: u8-mono-out ( r -- )
    1e f+ f2/ max_8_bit_volume f*
    f>s emit ;

: s16_le-mono-out ( r -- )
    f2/ max_16_bit_volume f* f>s
    dup emit
    256 / emit ;

: s16_be-mono-out ( r -- )
    f2/ max_16_bit_volume f* f>s
    dup 256 / emit
    emit ;

\ ---------------------------------------------------------------------------- \
\ Main
\ ---------------------------------------------------------------------------- \

44100e Fconstant cd_sample_rate
cd_sample_rate Fvalue sample-rate
sample-rate 1/f Fvalue sample-width

: set-sample-rate ( r -- )
    to sample-rate
    sample-rate 1/f to sample-width ;

Defer print-sample
' s16_le-mono-out is print-sample
: set-sample-size ( n -- )
    assert( dup 16 = over 8 = or )
    8 = if
        [ ' u8-mono-out ] literal is print-sample
    then ;

: tick-to-t ( n -- r )
    s>f sample-width f* ;

: main
    depth 0 > if
        set-sample-size
    then
    fdepth 0 > if
        set-sample-rate
    then
    sample-rate 1.1e f* f>s 0 do
        i tick-to-t
        sample-value
        print-sample
    loop
    assert( depth 0= )
    assert( fdepth 0= ) ;
