require ../lib/ttester.fs
require lpcm.fs

a-440-freq 1/f Fconstant a-440-period

1e-15 Fconstant epsilon
: epsilon+ ( r -- r )
    epsilon f+ ;

set-near

T{ 0e adsr -> 0e }T
T{ a f2/ adsr -> 0.5e }T
T{ a adsr -> 1e }T

T{ a d f2/ f+ adsr -> 1e s f+ f2/ }T
T{ a d f+ adsr -> s }T

T{ 0e a-440-freq saw-wave -> 0e }T
T{ a-440-period 4e f/ a-440-freq saw-wave -> 0.5e }T
T{ a-440-period 2e f/ a-440-freq saw-wave -> 1e }T
T{ a-440-period 2e f/ epsilon+ a-440-freq saw-wave -> -1e }T
T{ a-440-period a-440-freq saw-wave -> 0e }T
