require ../lib/ttester.fs
require lpcm.fs

set-near

T{ 0e adsr -> 0e }T
T{ a f2/ adsr -> 0.5e }T
T{ a adsr -> 1e }T

T{ a d f2/ f+ adsr -> 1e s f+ f2/ }T
T{ a d f+ adsr -> s }T
