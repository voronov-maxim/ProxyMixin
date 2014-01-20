
//  Microsoft (R) .NET Framework IL Disassembler.  Version 4.0.30319.33440
//  Copyright (c) Microsoft Corporation.  All rights reserved.



// Metadata version: v4.0.30319
.assembly extern mscorlib
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 4:0:0:0
}
.assembly CallMethodPointer.dll
{
  .hash algorithm 0x00008004
  .ver 1:0:0:0
}
.module CallMethodPointer.dll
// MVID: {0783D9C3-F9DD-43CA-B300-508A1E4DB7B8}
.imagebase 0x00400000
.file alignment 0x00000200
.stackreserve 0x00100000
.subsystem 0x0003       // WINDOWS_CUI
.corflags 0x00000001    //  ILONLY
// Image base: 0x00550000


// =============== CLASS MEMBERS DECLARATION ===================

.class public auto ansi sealed CallMethodPointer.IndirectInvokerAction0
       extends [mscorlib]System.Object
{
  .field private initonly native int methodPointer
  .method public specialname rtspecialname 
          instance void  .ctor(native int methodPointer) cil managed
  {
    // Code size       8 (0x8)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  ldarg.1
    IL_0002:  stfld      native int CallMethodPointer.IndirectInvokerAction0::methodPointer
    IL_0007:  ret
  } // end of method IndirectInvokerAction0::.ctor

  .method public static class CallMethodPointer.IndirectInvokerAction0 
          Create(native int methodPointer) cil managed
  {
    // Code size       7 (0x7)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  newobj     instance void CallMethodPointer.IndirectInvokerAction0::.ctor(native int)
    IL_0006:  ret
  } // end of method IndirectInvokerAction0::Create

  .method public static void  Invoke<T1>(native int target,
                                         !!T1 p1) cil managed
  {
    // Code size       13 (0xd)
    .maxstack  2
    IL_0000:  ldarg      p1
    IL_0004:  nop
    IL_0005:  nop
    IL_0006:  ldarg.0
    IL_0007:  calli      void(!!T1)
    IL_000c:  ret
  } // end of method IndirectInvokerAction0::Invoke

  .method public instance void  CarryingInvoke<T1>(!!T1 p1) cil managed
  {
    // Code size       18 (0x12)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  ldfld      native int CallMethodPointer.IndirectInvokerAction0::methodPointer
    IL_0006:  ldarg      p1
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  call       void CallMethodPointer.IndirectInvokerAction0::Invoke<!!0>(native int,
                                                                                    !!0)
    IL_0011:  ret
  } // end of method IndirectInvokerAction0::CarryingInvoke

} // end of class CallMethodPointer.IndirectInvokerAction0

.class public auto ansi sealed CallMethodPointer.IndirectInvokerFunc0
       extends [mscorlib]System.Object
{
  .field private initonly native int methodPointer
  .method public specialname rtspecialname 
          instance void  .ctor(native int methodPointer) cil managed
  {
    // Code size       8 (0x8)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  ldarg.1
    IL_0002:  stfld      native int CallMethodPointer.IndirectInvokerFunc0::methodPointer
    IL_0007:  ret
  } // end of method IndirectInvokerFunc0::.ctor

  .method public static class CallMethodPointer.IndirectInvokerFunc0 
          Create(native int methodPointer) cil managed
  {
    // Code size       7 (0x7)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  newobj     instance void CallMethodPointer.IndirectInvokerFunc0::.ctor(native int)
    IL_0006:  ret
  } // end of method IndirectInvokerFunc0::Create

  .method public static !!T2  Invoke<T1,T2>(native int target,
                                            !!T1 p1) cil managed
  {
    // Code size       13 (0xd)
    .maxstack  2
    IL_0000:  ldarg      p1
    IL_0004:  nop
    IL_0005:  nop
    IL_0006:  ldarg.0
    IL_0007:  calli      !!T2(!!T1)
    IL_000c:  ret
  } // end of method IndirectInvokerFunc0::Invoke

  .method public instance !!T2  CarryingInvoke<T1,T2>(!!T1 p1) cil managed
  {
    // Code size       18 (0x12)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  ldfld      native int CallMethodPointer.IndirectInvokerFunc0::methodPointer
    IL_0006:  ldarg      p1
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  call       !!1 CallMethodPointer.IndirectInvokerFunc0::Invoke<!!0,!!1>(native int,
                                                                                     !!0)
    IL_0011:  ret
  } // end of method IndirectInvokerFunc0::CarryingInvoke

} // end of class CallMethodPointer.IndirectInvokerFunc0

.class public auto ansi sealed CallMethodPointer.IndirectInvokerAction1
       extends [mscorlib]System.Object
{
  .field private initonly native int methodPointer
  .method public specialname rtspecialname 
          instance void  .ctor(native int methodPointer) cil managed
  {
    // Code size       8 (0x8)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  ldarg.1
    IL_0002:  stfld      native int CallMethodPointer.IndirectInvokerAction1::methodPointer
    IL_0007:  ret
  } // end of method IndirectInvokerAction1::.ctor

  .method public static class CallMethodPointer.IndirectInvokerAction1 
          Create(native int methodPointer) cil managed
  {
    // Code size       7 (0x7)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  newobj     instance void CallMethodPointer.IndirectInvokerAction1::.ctor(native int)
    IL_0006:  ret
  } // end of method IndirectInvokerAction1::Create

  .method public static void  Invoke<T1,T2>(native int target,
                                            !!T1 p1,
                                            !!T2 p2) cil managed
  {
    // Code size       19 (0x13)
    .maxstack  3
    IL_0000:  ldarg      p1
    IL_0004:  nop
    IL_0005:  nop
    IL_0006:  ldarg      p2
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg.0
    IL_000d:  calli      void(!!T1,!!T2)
    IL_0012:  ret
  } // end of method IndirectInvokerAction1::Invoke

  .method public instance void  CarryingInvoke<T1,T2>(!!T1 p1,
                                                      !!T2 p2) cil managed
  {
    // Code size       24 (0x18)
    .maxstack  3
    IL_0000:  ldarg.0
    IL_0001:  ldfld      native int CallMethodPointer.IndirectInvokerAction1::methodPointer
    IL_0006:  ldarg      p1
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p2
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  call       void CallMethodPointer.IndirectInvokerAction1::Invoke<!!0,!!1>(native int,
                                                                                        !!0,
                                                                                        !!1)
    IL_0017:  ret
  } // end of method IndirectInvokerAction1::CarryingInvoke

} // end of class CallMethodPointer.IndirectInvokerAction1

.class public auto ansi sealed CallMethodPointer.IndirectInvokerFunc1
       extends [mscorlib]System.Object
{
  .field private initonly native int methodPointer
  .method public specialname rtspecialname 
          instance void  .ctor(native int methodPointer) cil managed
  {
    // Code size       8 (0x8)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  ldarg.1
    IL_0002:  stfld      native int CallMethodPointer.IndirectInvokerFunc1::methodPointer
    IL_0007:  ret
  } // end of method IndirectInvokerFunc1::.ctor

  .method public static class CallMethodPointer.IndirectInvokerFunc1 
          Create(native int methodPointer) cil managed
  {
    // Code size       7 (0x7)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  newobj     instance void CallMethodPointer.IndirectInvokerFunc1::.ctor(native int)
    IL_0006:  ret
  } // end of method IndirectInvokerFunc1::Create

  .method public static !!T3  Invoke<T1,T2,T3>(native int target,
                                               !!T1 p1,
                                               !!T2 p2) cil managed
  {
    // Code size       19 (0x13)
    .maxstack  3
    IL_0000:  ldarg      p1
    IL_0004:  nop
    IL_0005:  nop
    IL_0006:  ldarg      p2
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg.0
    IL_000d:  calli      !!T3(!!T1,!!T2)
    IL_0012:  ret
  } // end of method IndirectInvokerFunc1::Invoke

  .method public instance !!T3  CarryingInvoke<T1,T2,T3>(!!T1 p1,
                                                         !!T2 p2) cil managed
  {
    // Code size       24 (0x18)
    .maxstack  3
    IL_0000:  ldarg.0
    IL_0001:  ldfld      native int CallMethodPointer.IndirectInvokerFunc1::methodPointer
    IL_0006:  ldarg      p1
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p2
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  call       !!2 CallMethodPointer.IndirectInvokerFunc1::Invoke<!!0,!!1,!!2>(native int,
                                                                                         !!0,
                                                                                         !!1)
    IL_0017:  ret
  } // end of method IndirectInvokerFunc1::CarryingInvoke

} // end of class CallMethodPointer.IndirectInvokerFunc1

.class public auto ansi sealed CallMethodPointer.IndirectInvokerAction2
       extends [mscorlib]System.Object
{
  .field private initonly native int methodPointer
  .method public specialname rtspecialname 
          instance void  .ctor(native int methodPointer) cil managed
  {
    // Code size       8 (0x8)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  ldarg.1
    IL_0002:  stfld      native int CallMethodPointer.IndirectInvokerAction2::methodPointer
    IL_0007:  ret
  } // end of method IndirectInvokerAction2::.ctor

  .method public static class CallMethodPointer.IndirectInvokerAction2 
          Create(native int methodPointer) cil managed
  {
    // Code size       7 (0x7)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  newobj     instance void CallMethodPointer.IndirectInvokerAction2::.ctor(native int)
    IL_0006:  ret
  } // end of method IndirectInvokerAction2::Create

  .method public static void  Invoke<T1,T2,T3>(native int target,
                                               !!T1 p1,
                                               !!T2 p2,
                                               !!T3 p3) cil managed
  {
    // Code size       25 (0x19)
    .maxstack  4
    IL_0000:  ldarg      p1
    IL_0004:  nop
    IL_0005:  nop
    IL_0006:  ldarg      p2
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p3
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg.0
    IL_0013:  calli      void(!!T1,!!T2,!!T3)
    IL_0018:  ret
  } // end of method IndirectInvokerAction2::Invoke

  .method public instance void  CarryingInvoke<T1,T2,T3>(!!T1 p1,
                                                         !!T2 p2,
                                                         !!T3 p3) cil managed
  {
    // Code size       30 (0x1e)
    .maxstack  4
    IL_0000:  ldarg.0
    IL_0001:  ldfld      native int CallMethodPointer.IndirectInvokerAction2::methodPointer
    IL_0006:  ldarg      p1
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p2
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg      p3
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  call       void CallMethodPointer.IndirectInvokerAction2::Invoke<!!0,!!1,!!2>(native int,
                                                                                            !!0,
                                                                                            !!1,
                                                                                            !!2)
    IL_001d:  ret
  } // end of method IndirectInvokerAction2::CarryingInvoke

} // end of class CallMethodPointer.IndirectInvokerAction2

.class public auto ansi sealed CallMethodPointer.IndirectInvokerFunc2
       extends [mscorlib]System.Object
{
  .field private initonly native int methodPointer
  .method public specialname rtspecialname 
          instance void  .ctor(native int methodPointer) cil managed
  {
    // Code size       8 (0x8)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  ldarg.1
    IL_0002:  stfld      native int CallMethodPointer.IndirectInvokerFunc2::methodPointer
    IL_0007:  ret
  } // end of method IndirectInvokerFunc2::.ctor

  .method public static class CallMethodPointer.IndirectInvokerFunc2 
          Create(native int methodPointer) cil managed
  {
    // Code size       7 (0x7)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  newobj     instance void CallMethodPointer.IndirectInvokerFunc2::.ctor(native int)
    IL_0006:  ret
  } // end of method IndirectInvokerFunc2::Create

  .method public static !!T4  Invoke<T1,T2,T3,T4>(native int target,
                                                  !!T1 p1,
                                                  !!T2 p2,
                                                  !!T3 p3) cil managed
  {
    // Code size       25 (0x19)
    .maxstack  4
    IL_0000:  ldarg      p1
    IL_0004:  nop
    IL_0005:  nop
    IL_0006:  ldarg      p2
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p3
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg.0
    IL_0013:  calli      !!T4(!!T1,!!T2,!!T3)
    IL_0018:  ret
  } // end of method IndirectInvokerFunc2::Invoke

  .method public instance !!T4  CarryingInvoke<T1,T2,T3,T4>(!!T1 p1,
                                                            !!T2 p2,
                                                            !!T3 p3) cil managed
  {
    // Code size       30 (0x1e)
    .maxstack  4
    IL_0000:  ldarg.0
    IL_0001:  ldfld      native int CallMethodPointer.IndirectInvokerFunc2::methodPointer
    IL_0006:  ldarg      p1
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p2
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg      p3
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  call       !!3 CallMethodPointer.IndirectInvokerFunc2::Invoke<!!0,!!1,!!2,!!3>(native int,
                                                                                             !!0,
                                                                                             !!1,
                                                                                             !!2)
    IL_001d:  ret
  } // end of method IndirectInvokerFunc2::CarryingInvoke

} // end of class CallMethodPointer.IndirectInvokerFunc2

.class public auto ansi sealed CallMethodPointer.IndirectInvokerAction3
       extends [mscorlib]System.Object
{
  .field private initonly native int methodPointer
  .method public specialname rtspecialname 
          instance void  .ctor(native int methodPointer) cil managed
  {
    // Code size       8 (0x8)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  ldarg.1
    IL_0002:  stfld      native int CallMethodPointer.IndirectInvokerAction3::methodPointer
    IL_0007:  ret
  } // end of method IndirectInvokerAction3::.ctor

  .method public static class CallMethodPointer.IndirectInvokerAction3 
          Create(native int methodPointer) cil managed
  {
    // Code size       7 (0x7)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  newobj     instance void CallMethodPointer.IndirectInvokerAction3::.ctor(native int)
    IL_0006:  ret
  } // end of method IndirectInvokerAction3::Create

  .method public static void  Invoke<T1,T2,T3,T4>(native int target,
                                                  !!T1 p1,
                                                  !!T2 p2,
                                                  !!T3 p3,
                                                  !!T4 p4) cil managed
  {
    // Code size       31 (0x1f)
    .maxstack  5
    IL_0000:  ldarg      p1
    IL_0004:  nop
    IL_0005:  nop
    IL_0006:  ldarg      p2
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p3
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg      p4
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  ldarg.0
    IL_0019:  calli      void(!!T1,!!T2,!!T3,!!T4)
    IL_001e:  ret
  } // end of method IndirectInvokerAction3::Invoke

  .method public instance void  CarryingInvoke<T1,T2,T3,T4>(!!T1 p1,
                                                            !!T2 p2,
                                                            !!T3 p3,
                                                            !!T4 p4) cil managed
  {
    // Code size       36 (0x24)
    .maxstack  5
    IL_0000:  ldarg.0
    IL_0001:  ldfld      native int CallMethodPointer.IndirectInvokerAction3::methodPointer
    IL_0006:  ldarg      p1
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p2
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg      p3
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  ldarg      p4
    IL_001c:  nop
    IL_001d:  nop
    IL_001e:  call       void CallMethodPointer.IndirectInvokerAction3::Invoke<!!0,!!1,!!2,!!3>(native int,
                                                                                                !!0,
                                                                                                !!1,
                                                                                                !!2,
                                                                                                !!3)
    IL_0023:  ret
  } // end of method IndirectInvokerAction3::CarryingInvoke

} // end of class CallMethodPointer.IndirectInvokerAction3

.class public auto ansi sealed CallMethodPointer.IndirectInvokerFunc3
       extends [mscorlib]System.Object
{
  .field private initonly native int methodPointer
  .method public specialname rtspecialname 
          instance void  .ctor(native int methodPointer) cil managed
  {
    // Code size       8 (0x8)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  ldarg.1
    IL_0002:  stfld      native int CallMethodPointer.IndirectInvokerFunc3::methodPointer
    IL_0007:  ret
  } // end of method IndirectInvokerFunc3::.ctor

  .method public static class CallMethodPointer.IndirectInvokerFunc3 
          Create(native int methodPointer) cil managed
  {
    // Code size       7 (0x7)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  newobj     instance void CallMethodPointer.IndirectInvokerFunc3::.ctor(native int)
    IL_0006:  ret
  } // end of method IndirectInvokerFunc3::Create

  .method public static !!T5  Invoke<T1,T2,T3,T4,T5>(native int target,
                                                     !!T1 p1,
                                                     !!T2 p2,
                                                     !!T3 p3,
                                                     !!T4 p4) cil managed
  {
    // Code size       31 (0x1f)
    .maxstack  5
    IL_0000:  ldarg      p1
    IL_0004:  nop
    IL_0005:  nop
    IL_0006:  ldarg      p2
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p3
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg      p4
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  ldarg.0
    IL_0019:  calli      !!T5(!!T1,!!T2,!!T3,!!T4)
    IL_001e:  ret
  } // end of method IndirectInvokerFunc3::Invoke

  .method public instance !!T5  CarryingInvoke<T1,T2,T3,T4,T5>(!!T1 p1,
                                                               !!T2 p2,
                                                               !!T3 p3,
                                                               !!T4 p4) cil managed
  {
    // Code size       36 (0x24)
    .maxstack  5
    IL_0000:  ldarg.0
    IL_0001:  ldfld      native int CallMethodPointer.IndirectInvokerFunc3::methodPointer
    IL_0006:  ldarg      p1
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p2
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg      p3
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  ldarg      p4
    IL_001c:  nop
    IL_001d:  nop
    IL_001e:  call       !!4 CallMethodPointer.IndirectInvokerFunc3::Invoke<!!0,!!1,!!2,!!3,!!4>(native int,
                                                                                                 !!0,
                                                                                                 !!1,
                                                                                                 !!2,
                                                                                                 !!3)
    IL_0023:  ret
  } // end of method IndirectInvokerFunc3::CarryingInvoke

} // end of class CallMethodPointer.IndirectInvokerFunc3

.class public auto ansi sealed CallMethodPointer.IndirectInvokerAction4
       extends [mscorlib]System.Object
{
  .field private initonly native int methodPointer
  .method public specialname rtspecialname 
          instance void  .ctor(native int methodPointer) cil managed
  {
    // Code size       8 (0x8)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  ldarg.1
    IL_0002:  stfld      native int CallMethodPointer.IndirectInvokerAction4::methodPointer
    IL_0007:  ret
  } // end of method IndirectInvokerAction4::.ctor

  .method public static class CallMethodPointer.IndirectInvokerAction4 
          Create(native int methodPointer) cil managed
  {
    // Code size       7 (0x7)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  newobj     instance void CallMethodPointer.IndirectInvokerAction4::.ctor(native int)
    IL_0006:  ret
  } // end of method IndirectInvokerAction4::Create

  .method public static void  Invoke<T1,T2,T3,T4,T5>(native int target,
                                                     !!T1 p1,
                                                     !!T2 p2,
                                                     !!T3 p3,
                                                     !!T4 p4,
                                                     !!T5 p5) cil managed
  {
    // Code size       37 (0x25)
    .maxstack  6
    IL_0000:  ldarg      p1
    IL_0004:  nop
    IL_0005:  nop
    IL_0006:  ldarg      p2
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p3
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg      p4
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  ldarg      p5
    IL_001c:  nop
    IL_001d:  nop
    IL_001e:  ldarg.0
    IL_001f:  calli      void(!!T1,!!T2,!!T3,!!T4,!!T5)
    IL_0024:  ret
  } // end of method IndirectInvokerAction4::Invoke

  .method public instance void  CarryingInvoke<T1,T2,T3,T4,T5>(!!T1 p1,
                                                               !!T2 p2,
                                                               !!T3 p3,
                                                               !!T4 p4,
                                                               !!T5 p5) cil managed
  {
    // Code size       42 (0x2a)
    .maxstack  6
    IL_0000:  ldarg.0
    IL_0001:  ldfld      native int CallMethodPointer.IndirectInvokerAction4::methodPointer
    IL_0006:  ldarg      p1
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p2
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg      p3
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  ldarg      p4
    IL_001c:  nop
    IL_001d:  nop
    IL_001e:  ldarg      p5
    IL_0022:  nop
    IL_0023:  nop
    IL_0024:  call       void CallMethodPointer.IndirectInvokerAction4::Invoke<!!0,!!1,!!2,!!3,!!4>(native int,
                                                                                                    !!0,
                                                                                                    !!1,
                                                                                                    !!2,
                                                                                                    !!3,
                                                                                                    !!4)
    IL_0029:  ret
  } // end of method IndirectInvokerAction4::CarryingInvoke

} // end of class CallMethodPointer.IndirectInvokerAction4

.class public auto ansi sealed CallMethodPointer.IndirectInvokerFunc4
       extends [mscorlib]System.Object
{
  .field private initonly native int methodPointer
  .method public specialname rtspecialname 
          instance void  .ctor(native int methodPointer) cil managed
  {
    // Code size       8 (0x8)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  ldarg.1
    IL_0002:  stfld      native int CallMethodPointer.IndirectInvokerFunc4::methodPointer
    IL_0007:  ret
  } // end of method IndirectInvokerFunc4::.ctor

  .method public static class CallMethodPointer.IndirectInvokerFunc4 
          Create(native int methodPointer) cil managed
  {
    // Code size       7 (0x7)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  newobj     instance void CallMethodPointer.IndirectInvokerFunc4::.ctor(native int)
    IL_0006:  ret
  } // end of method IndirectInvokerFunc4::Create

  .method public static !!T6  Invoke<T1,T2,T3,T4,T5,T6>(native int target,
                                                        !!T1 p1,
                                                        !!T2 p2,
                                                        !!T3 p3,
                                                        !!T4 p4,
                                                        !!T5 p5) cil managed
  {
    // Code size       37 (0x25)
    .maxstack  6
    IL_0000:  ldarg      p1
    IL_0004:  nop
    IL_0005:  nop
    IL_0006:  ldarg      p2
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p3
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg      p4
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  ldarg      p5
    IL_001c:  nop
    IL_001d:  nop
    IL_001e:  ldarg.0
    IL_001f:  calli      !!T6(!!T1,!!T2,!!T3,!!T4,!!T5)
    IL_0024:  ret
  } // end of method IndirectInvokerFunc4::Invoke

  .method public instance !!T6  CarryingInvoke<T1,T2,T3,T4,T5,T6>(!!T1 p1,
                                                                  !!T2 p2,
                                                                  !!T3 p3,
                                                                  !!T4 p4,
                                                                  !!T5 p5) cil managed
  {
    // Code size       42 (0x2a)
    .maxstack  6
    IL_0000:  ldarg.0
    IL_0001:  ldfld      native int CallMethodPointer.IndirectInvokerFunc4::methodPointer
    IL_0006:  ldarg      p1
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p2
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg      p3
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  ldarg      p4
    IL_001c:  nop
    IL_001d:  nop
    IL_001e:  ldarg      p5
    IL_0022:  nop
    IL_0023:  nop
    IL_0024:  call       !!5 CallMethodPointer.IndirectInvokerFunc4::Invoke<!!0,!!1,!!2,!!3,!!4,!!5>(native int,
                                                                                                     !!0,
                                                                                                     !!1,
                                                                                                     !!2,
                                                                                                     !!3,
                                                                                                     !!4)
    IL_0029:  ret
  } // end of method IndirectInvokerFunc4::CarryingInvoke

} // end of class CallMethodPointer.IndirectInvokerFunc4

.class public auto ansi sealed CallMethodPointer.IndirectInvokerAction5
       extends [mscorlib]System.Object
{
  .field private initonly native int methodPointer
  .method public specialname rtspecialname 
          instance void  .ctor(native int methodPointer) cil managed
  {
    // Code size       8 (0x8)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  ldarg.1
    IL_0002:  stfld      native int CallMethodPointer.IndirectInvokerAction5::methodPointer
    IL_0007:  ret
  } // end of method IndirectInvokerAction5::.ctor

  .method public static class CallMethodPointer.IndirectInvokerAction5 
          Create(native int methodPointer) cil managed
  {
    // Code size       7 (0x7)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  newobj     instance void CallMethodPointer.IndirectInvokerAction5::.ctor(native int)
    IL_0006:  ret
  } // end of method IndirectInvokerAction5::Create

  .method public static void  Invoke<T1,T2,T3,T4,T5,T6>(native int target,
                                                        !!T1 p1,
                                                        !!T2 p2,
                                                        !!T3 p3,
                                                        !!T4 p4,
                                                        !!T5 p5,
                                                        !!T6 p6) cil managed
  {
    // Code size       43 (0x2b)
    .maxstack  7
    IL_0000:  ldarg      p1
    IL_0004:  nop
    IL_0005:  nop
    IL_0006:  ldarg      p2
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p3
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg      p4
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  ldarg      p5
    IL_001c:  nop
    IL_001d:  nop
    IL_001e:  ldarg      p6
    IL_0022:  nop
    IL_0023:  nop
    IL_0024:  ldarg.0
    IL_0025:  calli      void(!!T1,!!T2,!!T3,!!T4,!!T5,!!T6)
    IL_002a:  ret
  } // end of method IndirectInvokerAction5::Invoke

  .method public instance void  CarryingInvoke<T1,T2,T3,T4,T5,T6>(!!T1 p1,
                                                                  !!T2 p2,
                                                                  !!T3 p3,
                                                                  !!T4 p4,
                                                                  !!T5 p5,
                                                                  !!T6 p6) cil managed
  {
    // Code size       48 (0x30)
    .maxstack  7
    IL_0000:  ldarg.0
    IL_0001:  ldfld      native int CallMethodPointer.IndirectInvokerAction5::methodPointer
    IL_0006:  ldarg      p1
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p2
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg      p3
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  ldarg      p4
    IL_001c:  nop
    IL_001d:  nop
    IL_001e:  ldarg      p5
    IL_0022:  nop
    IL_0023:  nop
    IL_0024:  ldarg      p6
    IL_0028:  nop
    IL_0029:  nop
    IL_002a:  call       void CallMethodPointer.IndirectInvokerAction5::Invoke<!!0,!!1,!!2,!!3,!!4,!!5>(native int,
                                                                                                        !!0,
                                                                                                        !!1,
                                                                                                        !!2,
                                                                                                        !!3,
                                                                                                        !!4,
                                                                                                        !!5)
    IL_002f:  ret
  } // end of method IndirectInvokerAction5::CarryingInvoke

} // end of class CallMethodPointer.IndirectInvokerAction5

.class public auto ansi sealed CallMethodPointer.IndirectInvokerFunc5
       extends [mscorlib]System.Object
{
  .field private initonly native int methodPointer
  .method public specialname rtspecialname 
          instance void  .ctor(native int methodPointer) cil managed
  {
    // Code size       8 (0x8)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  ldarg.1
    IL_0002:  stfld      native int CallMethodPointer.IndirectInvokerFunc5::methodPointer
    IL_0007:  ret
  } // end of method IndirectInvokerFunc5::.ctor

  .method public static class CallMethodPointer.IndirectInvokerFunc5 
          Create(native int methodPointer) cil managed
  {
    // Code size       7 (0x7)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  newobj     instance void CallMethodPointer.IndirectInvokerFunc5::.ctor(native int)
    IL_0006:  ret
  } // end of method IndirectInvokerFunc5::Create

  .method public static !!T7  Invoke<T1,T2,T3,T4,T5,T6,T7>(native int target,
                                                           !!T1 p1,
                                                           !!T2 p2,
                                                           !!T3 p3,
                                                           !!T4 p4,
                                                           !!T5 p5,
                                                           !!T6 p6) cil managed
  {
    // Code size       43 (0x2b)
    .maxstack  7
    IL_0000:  ldarg      p1
    IL_0004:  nop
    IL_0005:  nop
    IL_0006:  ldarg      p2
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p3
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg      p4
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  ldarg      p5
    IL_001c:  nop
    IL_001d:  nop
    IL_001e:  ldarg      p6
    IL_0022:  nop
    IL_0023:  nop
    IL_0024:  ldarg.0
    IL_0025:  calli      !!T7(!!T1,!!T2,!!T3,!!T4,!!T5,!!T6)
    IL_002a:  ret
  } // end of method IndirectInvokerFunc5::Invoke

  .method public instance !!T7  CarryingInvoke<T1,T2,T3,T4,T5,T6,T7>(!!T1 p1,
                                                                     !!T2 p2,
                                                                     !!T3 p3,
                                                                     !!T4 p4,
                                                                     !!T5 p5,
                                                                     !!T6 p6) cil managed
  {
    // Code size       48 (0x30)
    .maxstack  7
    IL_0000:  ldarg.0
    IL_0001:  ldfld      native int CallMethodPointer.IndirectInvokerFunc5::methodPointer
    IL_0006:  ldarg      p1
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p2
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg      p3
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  ldarg      p4
    IL_001c:  nop
    IL_001d:  nop
    IL_001e:  ldarg      p5
    IL_0022:  nop
    IL_0023:  nop
    IL_0024:  ldarg      p6
    IL_0028:  nop
    IL_0029:  nop
    IL_002a:  call       !!6 CallMethodPointer.IndirectInvokerFunc5::Invoke<!!0,!!1,!!2,!!3,!!4,!!5,!!6>(native int,
                                                                                                         !!0,
                                                                                                         !!1,
                                                                                                         !!2,
                                                                                                         !!3,
                                                                                                         !!4,
                                                                                                         !!5)
    IL_002f:  ret
  } // end of method IndirectInvokerFunc5::CarryingInvoke

} // end of class CallMethodPointer.IndirectInvokerFunc5

.class public auto ansi sealed CallMethodPointer.IndirectInvokerAction6
       extends [mscorlib]System.Object
{
  .field private initonly native int methodPointer
  .method public specialname rtspecialname 
          instance void  .ctor(native int methodPointer) cil managed
  {
    // Code size       8 (0x8)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  ldarg.1
    IL_0002:  stfld      native int CallMethodPointer.IndirectInvokerAction6::methodPointer
    IL_0007:  ret
  } // end of method IndirectInvokerAction6::.ctor

  .method public static class CallMethodPointer.IndirectInvokerAction6 
          Create(native int methodPointer) cil managed
  {
    // Code size       7 (0x7)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  newobj     instance void CallMethodPointer.IndirectInvokerAction6::.ctor(native int)
    IL_0006:  ret
  } // end of method IndirectInvokerAction6::Create

  .method public static void  Invoke<T1,T2,T3,T4,T5,T6,T7>(native int target,
                                                           !!T1 p1,
                                                           !!T2 p2,
                                                           !!T3 p3,
                                                           !!T4 p4,
                                                           !!T5 p5,
                                                           !!T6 p6,
                                                           !!T7 p7) cil managed
  {
    // Code size       49 (0x31)
    .maxstack  8
    IL_0000:  ldarg      p1
    IL_0004:  nop
    IL_0005:  nop
    IL_0006:  ldarg      p2
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p3
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg      p4
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  ldarg      p5
    IL_001c:  nop
    IL_001d:  nop
    IL_001e:  ldarg      p6
    IL_0022:  nop
    IL_0023:  nop
    IL_0024:  ldarg      p7
    IL_0028:  nop
    IL_0029:  nop
    IL_002a:  ldarg.0
    IL_002b:  calli      void(!!T1,!!T2,!!T3,!!T4,!!T5,!!T6,!!T7)
    IL_0030:  ret
  } // end of method IndirectInvokerAction6::Invoke

  .method public instance void  CarryingInvoke<T1,T2,T3,T4,T5,T6,T7>(!!T1 p1,
                                                                     !!T2 p2,
                                                                     !!T3 p3,
                                                                     !!T4 p4,
                                                                     !!T5 p5,
                                                                     !!T6 p6,
                                                                     !!T7 p7) cil managed
  {
    // Code size       54 (0x36)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  ldfld      native int CallMethodPointer.IndirectInvokerAction6::methodPointer
    IL_0006:  ldarg      p1
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p2
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg      p3
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  ldarg      p4
    IL_001c:  nop
    IL_001d:  nop
    IL_001e:  ldarg      p5
    IL_0022:  nop
    IL_0023:  nop
    IL_0024:  ldarg      p6
    IL_0028:  nop
    IL_0029:  nop
    IL_002a:  ldarg      p7
    IL_002e:  nop
    IL_002f:  nop
    IL_0030:  call       void CallMethodPointer.IndirectInvokerAction6::Invoke<!!0,!!1,!!2,!!3,!!4,!!5,!!6>(native int,
                                                                                                            !!0,
                                                                                                            !!1,
                                                                                                            !!2,
                                                                                                            !!3,
                                                                                                            !!4,
                                                                                                            !!5,
                                                                                                            !!6)
    IL_0035:  ret
  } // end of method IndirectInvokerAction6::CarryingInvoke

} // end of class CallMethodPointer.IndirectInvokerAction6

.class public auto ansi sealed CallMethodPointer.IndirectInvokerFunc6
       extends [mscorlib]System.Object
{
  .field private initonly native int methodPointer
  .method public specialname rtspecialname 
          instance void  .ctor(native int methodPointer) cil managed
  {
    // Code size       8 (0x8)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  ldarg.1
    IL_0002:  stfld      native int CallMethodPointer.IndirectInvokerFunc6::methodPointer
    IL_0007:  ret
  } // end of method IndirectInvokerFunc6::.ctor

  .method public static class CallMethodPointer.IndirectInvokerFunc6 
          Create(native int methodPointer) cil managed
  {
    // Code size       7 (0x7)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  newobj     instance void CallMethodPointer.IndirectInvokerFunc6::.ctor(native int)
    IL_0006:  ret
  } // end of method IndirectInvokerFunc6::Create

  .method public static !!T8  Invoke<T1,T2,T3,T4,T5,T6,T7,T8>(native int target,
                                                              !!T1 p1,
                                                              !!T2 p2,
                                                              !!T3 p3,
                                                              !!T4 p4,
                                                              !!T5 p5,
                                                              !!T6 p6,
                                                              !!T7 p7) cil managed
  {
    // Code size       49 (0x31)
    .maxstack  8
    IL_0000:  ldarg      p1
    IL_0004:  nop
    IL_0005:  nop
    IL_0006:  ldarg      p2
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p3
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg      p4
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  ldarg      p5
    IL_001c:  nop
    IL_001d:  nop
    IL_001e:  ldarg      p6
    IL_0022:  nop
    IL_0023:  nop
    IL_0024:  ldarg      p7
    IL_0028:  nop
    IL_0029:  nop
    IL_002a:  ldarg.0
    IL_002b:  calli      !!T8(!!T1,!!T2,!!T3,!!T4,!!T5,!!T6,!!T7)
    IL_0030:  ret
  } // end of method IndirectInvokerFunc6::Invoke

  .method public instance !!T8  CarryingInvoke<T1,T2,T3,T4,T5,T6,T7,T8>(!!T1 p1,
                                                                        !!T2 p2,
                                                                        !!T3 p3,
                                                                        !!T4 p4,
                                                                        !!T5 p5,
                                                                        !!T6 p6,
                                                                        !!T7 p7) cil managed
  {
    // Code size       54 (0x36)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  ldfld      native int CallMethodPointer.IndirectInvokerFunc6::methodPointer
    IL_0006:  ldarg      p1
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p2
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg      p3
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  ldarg      p4
    IL_001c:  nop
    IL_001d:  nop
    IL_001e:  ldarg      p5
    IL_0022:  nop
    IL_0023:  nop
    IL_0024:  ldarg      p6
    IL_0028:  nop
    IL_0029:  nop
    IL_002a:  ldarg      p7
    IL_002e:  nop
    IL_002f:  nop
    IL_0030:  call       !!7 CallMethodPointer.IndirectInvokerFunc6::Invoke<!!0,!!1,!!2,!!3,!!4,!!5,!!6,!!7>(native int,
                                                                                                             !!0,
                                                                                                             !!1,
                                                                                                             !!2,
                                                                                                             !!3,
                                                                                                             !!4,
                                                                                                             !!5,
                                                                                                             !!6)
    IL_0035:  ret
  } // end of method IndirectInvokerFunc6::CarryingInvoke

} // end of class CallMethodPointer.IndirectInvokerFunc6

.class public auto ansi sealed CallMethodPointer.IndirectInvokerAction7
       extends [mscorlib]System.Object
{
  .field private initonly native int methodPointer
  .method public specialname rtspecialname 
          instance void  .ctor(native int methodPointer) cil managed
  {
    // Code size       8 (0x8)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  ldarg.1
    IL_0002:  stfld      native int CallMethodPointer.IndirectInvokerAction7::methodPointer
    IL_0007:  ret
  } // end of method IndirectInvokerAction7::.ctor

  .method public static class CallMethodPointer.IndirectInvokerAction7 
          Create(native int methodPointer) cil managed
  {
    // Code size       7 (0x7)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  newobj     instance void CallMethodPointer.IndirectInvokerAction7::.ctor(native int)
    IL_0006:  ret
  } // end of method IndirectInvokerAction7::Create

  .method public static void  Invoke<T1,T2,T3,T4,T5,T6,T7,T8>(native int target,
                                                              !!T1 p1,
                                                              !!T2 p2,
                                                              !!T3 p3,
                                                              !!T4 p4,
                                                              !!T5 p5,
                                                              !!T6 p6,
                                                              !!T7 p7,
                                                              !!T8 p8) cil managed
  {
    // Code size       55 (0x37)
    .maxstack  9
    IL_0000:  ldarg      p1
    IL_0004:  nop
    IL_0005:  nop
    IL_0006:  ldarg      p2
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p3
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg      p4
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  ldarg      p5
    IL_001c:  nop
    IL_001d:  nop
    IL_001e:  ldarg      p6
    IL_0022:  nop
    IL_0023:  nop
    IL_0024:  ldarg      p7
    IL_0028:  nop
    IL_0029:  nop
    IL_002a:  ldarg      p8
    IL_002e:  nop
    IL_002f:  nop
    IL_0030:  ldarg.0
    IL_0031:  calli      void(!!T1,!!T2,!!T3,!!T4,!!T5,!!T6,!!T7,!!T8)
    IL_0036:  ret
  } // end of method IndirectInvokerAction7::Invoke

  .method public instance void  CarryingInvoke<T1,T2,T3,T4,T5,T6,T7,T8>(!!T1 p1,
                                                                        !!T2 p2,
                                                                        !!T3 p3,
                                                                        !!T4 p4,
                                                                        !!T5 p5,
                                                                        !!T6 p6,
                                                                        !!T7 p7,
                                                                        !!T8 p8) cil managed
  {
    // Code size       60 (0x3c)
    .maxstack  9
    IL_0000:  ldarg.0
    IL_0001:  ldfld      native int CallMethodPointer.IndirectInvokerAction7::methodPointer
    IL_0006:  ldarg      p1
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p2
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg      p3
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  ldarg      p4
    IL_001c:  nop
    IL_001d:  nop
    IL_001e:  ldarg      p5
    IL_0022:  nop
    IL_0023:  nop
    IL_0024:  ldarg      p6
    IL_0028:  nop
    IL_0029:  nop
    IL_002a:  ldarg      p7
    IL_002e:  nop
    IL_002f:  nop
    IL_0030:  ldarg      p8
    IL_0034:  nop
    IL_0035:  nop
    IL_0036:  call       void CallMethodPointer.IndirectInvokerAction7::Invoke<!!0,!!1,!!2,!!3,!!4,!!5,!!6,!!7>(native int,
                                                                                                                !!0,
                                                                                                                !!1,
                                                                                                                !!2,
                                                                                                                !!3,
                                                                                                                !!4,
                                                                                                                !!5,
                                                                                                                !!6,
                                                                                                                !!7)
    IL_003b:  ret
  } // end of method IndirectInvokerAction7::CarryingInvoke

} // end of class CallMethodPointer.IndirectInvokerAction7

.class public auto ansi sealed CallMethodPointer.IndirectInvokerFunc7
       extends [mscorlib]System.Object
{
  .field private initonly native int methodPointer
  .method public specialname rtspecialname 
          instance void  .ctor(native int methodPointer) cil managed
  {
    // Code size       8 (0x8)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  ldarg.1
    IL_0002:  stfld      native int CallMethodPointer.IndirectInvokerFunc7::methodPointer
    IL_0007:  ret
  } // end of method IndirectInvokerFunc7::.ctor

  .method public static class CallMethodPointer.IndirectInvokerFunc7 
          Create(native int methodPointer) cil managed
  {
    // Code size       7 (0x7)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  newobj     instance void CallMethodPointer.IndirectInvokerFunc7::.ctor(native int)
    IL_0006:  ret
  } // end of method IndirectInvokerFunc7::Create

  .method public static !!T9  Invoke<T1,T2,T3,T4,T5,T6,T7,T8,T9>(native int target,
                                                                 !!T1 p1,
                                                                 !!T2 p2,
                                                                 !!T3 p3,
                                                                 !!T4 p4,
                                                                 !!T5 p5,
                                                                 !!T6 p6,
                                                                 !!T7 p7,
                                                                 !!T8 p8) cil managed
  {
    // Code size       55 (0x37)
    .maxstack  9
    IL_0000:  ldarg      p1
    IL_0004:  nop
    IL_0005:  nop
    IL_0006:  ldarg      p2
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p3
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg      p4
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  ldarg      p5
    IL_001c:  nop
    IL_001d:  nop
    IL_001e:  ldarg      p6
    IL_0022:  nop
    IL_0023:  nop
    IL_0024:  ldarg      p7
    IL_0028:  nop
    IL_0029:  nop
    IL_002a:  ldarg      p8
    IL_002e:  nop
    IL_002f:  nop
    IL_0030:  ldarg.0
    IL_0031:  calli      !!T9(!!T1,!!T2,!!T3,!!T4,!!T5,!!T6,!!T7,!!T8)
    IL_0036:  ret
  } // end of method IndirectInvokerFunc7::Invoke

  .method public instance !!T9  CarryingInvoke<T1,T2,T3,T4,T5,T6,T7,T8,T9>(!!T1 p1,
                                                                           !!T2 p2,
                                                                           !!T3 p3,
                                                                           !!T4 p4,
                                                                           !!T5 p5,
                                                                           !!T6 p6,
                                                                           !!T7 p7,
                                                                           !!T8 p8) cil managed
  {
    // Code size       60 (0x3c)
    .maxstack  9
    IL_0000:  ldarg.0
    IL_0001:  ldfld      native int CallMethodPointer.IndirectInvokerFunc7::methodPointer
    IL_0006:  ldarg      p1
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p2
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg      p3
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  ldarg      p4
    IL_001c:  nop
    IL_001d:  nop
    IL_001e:  ldarg      p5
    IL_0022:  nop
    IL_0023:  nop
    IL_0024:  ldarg      p6
    IL_0028:  nop
    IL_0029:  nop
    IL_002a:  ldarg      p7
    IL_002e:  nop
    IL_002f:  nop
    IL_0030:  ldarg      p8
    IL_0034:  nop
    IL_0035:  nop
    IL_0036:  call       !!8 CallMethodPointer.IndirectInvokerFunc7::Invoke<!!0,!!1,!!2,!!3,!!4,!!5,!!6,!!7,!!8>(native int,
                                                                                                                 !!0,
                                                                                                                 !!1,
                                                                                                                 !!2,
                                                                                                                 !!3,
                                                                                                                 !!4,
                                                                                                                 !!5,
                                                                                                                 !!6,
                                                                                                                 !!7)
    IL_003b:  ret
  } // end of method IndirectInvokerFunc7::CarryingInvoke

} // end of class CallMethodPointer.IndirectInvokerFunc7

.class public auto ansi sealed CallMethodPointer.IndirectInvokerAction8
       extends [mscorlib]System.Object
{
  .field private initonly native int methodPointer
  .method public specialname rtspecialname 
          instance void  .ctor(native int methodPointer) cil managed
  {
    // Code size       8 (0x8)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  ldarg.1
    IL_0002:  stfld      native int CallMethodPointer.IndirectInvokerAction8::methodPointer
    IL_0007:  ret
  } // end of method IndirectInvokerAction8::.ctor

  .method public static class CallMethodPointer.IndirectInvokerAction8 
          Create(native int methodPointer) cil managed
  {
    // Code size       7 (0x7)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  newobj     instance void CallMethodPointer.IndirectInvokerAction8::.ctor(native int)
    IL_0006:  ret
  } // end of method IndirectInvokerAction8::Create

  .method public static void  Invoke<T1,T2,T3,T4,T5,T6,T7,T8,T9>(native int target,
                                                                 !!T1 p1,
                                                                 !!T2 p2,
                                                                 !!T3 p3,
                                                                 !!T4 p4,
                                                                 !!T5 p5,
                                                                 !!T6 p6,
                                                                 !!T7 p7,
                                                                 !!T8 p8,
                                                                 !!T9 p9) cil managed
  {
    // Code size       61 (0x3d)
    .maxstack  10
    IL_0000:  ldarg      p1
    IL_0004:  nop
    IL_0005:  nop
    IL_0006:  ldarg      p2
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p3
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg      p4
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  ldarg      p5
    IL_001c:  nop
    IL_001d:  nop
    IL_001e:  ldarg      p6
    IL_0022:  nop
    IL_0023:  nop
    IL_0024:  ldarg      p7
    IL_0028:  nop
    IL_0029:  nop
    IL_002a:  ldarg      p8
    IL_002e:  nop
    IL_002f:  nop
    IL_0030:  ldarg      p9
    IL_0034:  nop
    IL_0035:  nop
    IL_0036:  ldarg.0
    IL_0037:  calli      void(!!T1,!!T2,!!T3,!!T4,!!T5,!!T6,!!T7,!!T8,!!T9)
    IL_003c:  ret
  } // end of method IndirectInvokerAction8::Invoke

  .method public instance void  CarryingInvoke<T1,T2,T3,T4,T5,T6,T7,T8,T9>(!!T1 p1,
                                                                           !!T2 p2,
                                                                           !!T3 p3,
                                                                           !!T4 p4,
                                                                           !!T5 p5,
                                                                           !!T6 p6,
                                                                           !!T7 p7,
                                                                           !!T8 p8,
                                                                           !!T9 p9) cil managed
  {
    // Code size       66 (0x42)
    .maxstack  10
    IL_0000:  ldarg.0
    IL_0001:  ldfld      native int CallMethodPointer.IndirectInvokerAction8::methodPointer
    IL_0006:  ldarg      p1
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p2
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg      p3
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  ldarg      p4
    IL_001c:  nop
    IL_001d:  nop
    IL_001e:  ldarg      p5
    IL_0022:  nop
    IL_0023:  nop
    IL_0024:  ldarg      p6
    IL_0028:  nop
    IL_0029:  nop
    IL_002a:  ldarg      p7
    IL_002e:  nop
    IL_002f:  nop
    IL_0030:  ldarg      p8
    IL_0034:  nop
    IL_0035:  nop
    IL_0036:  ldarg      p9
    IL_003a:  nop
    IL_003b:  nop
    IL_003c:  call       void CallMethodPointer.IndirectInvokerAction8::Invoke<!!0,!!1,!!2,!!3,!!4,!!5,!!6,!!7,!!8>(native int,
                                                                                                                    !!0,
                                                                                                                    !!1,
                                                                                                                    !!2,
                                                                                                                    !!3,
                                                                                                                    !!4,
                                                                                                                    !!5,
                                                                                                                    !!6,
                                                                                                                    !!7,
                                                                                                                    !!8)
    IL_0041:  ret
  } // end of method IndirectInvokerAction8::CarryingInvoke

} // end of class CallMethodPointer.IndirectInvokerAction8

.class public auto ansi sealed CallMethodPointer.IndirectInvokerFunc8
       extends [mscorlib]System.Object
{
  .field private initonly native int methodPointer
  .method public specialname rtspecialname 
          instance void  .ctor(native int methodPointer) cil managed
  {
    // Code size       8 (0x8)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  ldarg.1
    IL_0002:  stfld      native int CallMethodPointer.IndirectInvokerFunc8::methodPointer
    IL_0007:  ret
  } // end of method IndirectInvokerFunc8::.ctor

  .method public static class CallMethodPointer.IndirectInvokerFunc8 
          Create(native int methodPointer) cil managed
  {
    // Code size       7 (0x7)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  newobj     instance void CallMethodPointer.IndirectInvokerFunc8::.ctor(native int)
    IL_0006:  ret
  } // end of method IndirectInvokerFunc8::Create

  .method public static !!T10  Invoke<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10>(native int target,
                                                                      !!T1 p1,
                                                                      !!T2 p2,
                                                                      !!T3 p3,
                                                                      !!T4 p4,
                                                                      !!T5 p5,
                                                                      !!T6 p6,
                                                                      !!T7 p7,
                                                                      !!T8 p8,
                                                                      !!T9 p9) cil managed
  {
    // Code size       61 (0x3d)
    .maxstack  10
    IL_0000:  ldarg      p1
    IL_0004:  nop
    IL_0005:  nop
    IL_0006:  ldarg      p2
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p3
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg      p4
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  ldarg      p5
    IL_001c:  nop
    IL_001d:  nop
    IL_001e:  ldarg      p6
    IL_0022:  nop
    IL_0023:  nop
    IL_0024:  ldarg      p7
    IL_0028:  nop
    IL_0029:  nop
    IL_002a:  ldarg      p8
    IL_002e:  nop
    IL_002f:  nop
    IL_0030:  ldarg      p9
    IL_0034:  nop
    IL_0035:  nop
    IL_0036:  ldarg.0
    IL_0037:  calli      !!T10(!!T1,!!T2,!!T3,!!T4,!!T5,!!T6,!!T7,!!T8,!!T9)
    IL_003c:  ret
  } // end of method IndirectInvokerFunc8::Invoke

  .method public instance !!T10  CarryingInvoke<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10>(!!T1 p1,
                                                                                !!T2 p2,
                                                                                !!T3 p3,
                                                                                !!T4 p4,
                                                                                !!T5 p5,
                                                                                !!T6 p6,
                                                                                !!T7 p7,
                                                                                !!T8 p8,
                                                                                !!T9 p9) cil managed
  {
    // Code size       66 (0x42)
    .maxstack  10
    IL_0000:  ldarg.0
    IL_0001:  ldfld      native int CallMethodPointer.IndirectInvokerFunc8::methodPointer
    IL_0006:  ldarg      p1
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p2
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg      p3
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  ldarg      p4
    IL_001c:  nop
    IL_001d:  nop
    IL_001e:  ldarg      p5
    IL_0022:  nop
    IL_0023:  nop
    IL_0024:  ldarg      p6
    IL_0028:  nop
    IL_0029:  nop
    IL_002a:  ldarg      p7
    IL_002e:  nop
    IL_002f:  nop
    IL_0030:  ldarg      p8
    IL_0034:  nop
    IL_0035:  nop
    IL_0036:  ldarg      p9
    IL_003a:  nop
    IL_003b:  nop
    IL_003c:  call       !!9 CallMethodPointer.IndirectInvokerFunc8::Invoke<!!0,!!1,!!2,!!3,!!4,!!5,!!6,!!7,!!8,!!9>(native int,
                                                                                                                     !!0,
                                                                                                                     !!1,
                                                                                                                     !!2,
                                                                                                                     !!3,
                                                                                                                     !!4,
                                                                                                                     !!5,
                                                                                                                     !!6,
                                                                                                                     !!7,
                                                                                                                     !!8)
    IL_0041:  ret
  } // end of method IndirectInvokerFunc8::CarryingInvoke

} // end of class CallMethodPointer.IndirectInvokerFunc8

.class public auto ansi sealed CallMethodPointer.IndirectInvokerAction9
       extends [mscorlib]System.Object
{
  .field private initonly native int methodPointer
  .method public specialname rtspecialname 
          instance void  .ctor(native int methodPointer) cil managed
  {
    // Code size       8 (0x8)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  ldarg.1
    IL_0002:  stfld      native int CallMethodPointer.IndirectInvokerAction9::methodPointer
    IL_0007:  ret
  } // end of method IndirectInvokerAction9::.ctor

  .method public static class CallMethodPointer.IndirectInvokerAction9 
          Create(native int methodPointer) cil managed
  {
    // Code size       7 (0x7)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  newobj     instance void CallMethodPointer.IndirectInvokerAction9::.ctor(native int)
    IL_0006:  ret
  } // end of method IndirectInvokerAction9::Create

  .method public static void  Invoke<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10>(native int target,
                                                                     !!T1 p1,
                                                                     !!T2 p2,
                                                                     !!T3 p3,
                                                                     !!T4 p4,
                                                                     !!T5 p5,
                                                                     !!T6 p6,
                                                                     !!T7 p7,
                                                                     !!T8 p8,
                                                                     !!T9 p9,
                                                                     !!T10 p10) cil managed
  {
    // Code size       67 (0x43)
    .maxstack  11
    IL_0000:  ldarg      p1
    IL_0004:  nop
    IL_0005:  nop
    IL_0006:  ldarg      p2
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p3
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg      p4
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  ldarg      p5
    IL_001c:  nop
    IL_001d:  nop
    IL_001e:  ldarg      p6
    IL_0022:  nop
    IL_0023:  nop
    IL_0024:  ldarg      p7
    IL_0028:  nop
    IL_0029:  nop
    IL_002a:  ldarg      p8
    IL_002e:  nop
    IL_002f:  nop
    IL_0030:  ldarg      p9
    IL_0034:  nop
    IL_0035:  nop
    IL_0036:  ldarg      p10
    IL_003a:  nop
    IL_003b:  nop
    IL_003c:  ldarg.0
    IL_003d:  calli      void(!!T1,!!T2,!!T3,!!T4,!!T5,!!T6,!!T7,!!T8,!!T9,!!T10)
    IL_0042:  ret
  } // end of method IndirectInvokerAction9::Invoke

  .method public instance void  CarryingInvoke<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10>(!!T1 p1,
                                                                               !!T2 p2,
                                                                               !!T3 p3,
                                                                               !!T4 p4,
                                                                               !!T5 p5,
                                                                               !!T6 p6,
                                                                               !!T7 p7,
                                                                               !!T8 p8,
                                                                               !!T9 p9,
                                                                               !!T10 p10) cil managed
  {
    // Code size       72 (0x48)
    .maxstack  11
    IL_0000:  ldarg.0
    IL_0001:  ldfld      native int CallMethodPointer.IndirectInvokerAction9::methodPointer
    IL_0006:  ldarg      p1
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p2
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg      p3
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  ldarg      p4
    IL_001c:  nop
    IL_001d:  nop
    IL_001e:  ldarg      p5
    IL_0022:  nop
    IL_0023:  nop
    IL_0024:  ldarg      p6
    IL_0028:  nop
    IL_0029:  nop
    IL_002a:  ldarg      p7
    IL_002e:  nop
    IL_002f:  nop
    IL_0030:  ldarg      p8
    IL_0034:  nop
    IL_0035:  nop
    IL_0036:  ldarg      p9
    IL_003a:  nop
    IL_003b:  nop
    IL_003c:  ldarg      p10
    IL_0040:  nop
    IL_0041:  nop
    IL_0042:  call       void CallMethodPointer.IndirectInvokerAction9::Invoke<!!0,!!1,!!2,!!3,!!4,!!5,!!6,!!7,!!8,!!9>(native int,
                                                                                                                        !!0,
                                                                                                                        !!1,
                                                                                                                        !!2,
                                                                                                                        !!3,
                                                                                                                        !!4,
                                                                                                                        !!5,
                                                                                                                        !!6,
                                                                                                                        !!7,
                                                                                                                        !!8,
                                                                                                                        !!9)
    IL_0047:  ret
  } // end of method IndirectInvokerAction9::CarryingInvoke

} // end of class CallMethodPointer.IndirectInvokerAction9

.class public auto ansi sealed CallMethodPointer.IndirectInvokerFunc9
       extends [mscorlib]System.Object
{
  .field private initonly native int methodPointer
  .method public specialname rtspecialname 
          instance void  .ctor(native int methodPointer) cil managed
  {
    // Code size       8 (0x8)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  ldarg.1
    IL_0002:  stfld      native int CallMethodPointer.IndirectInvokerFunc9::methodPointer
    IL_0007:  ret
  } // end of method IndirectInvokerFunc9::.ctor

  .method public static class CallMethodPointer.IndirectInvokerFunc9 
          Create(native int methodPointer) cil managed
  {
    // Code size       7 (0x7)
    .maxstack  2
    IL_0000:  ldarg.0
    IL_0001:  newobj     instance void CallMethodPointer.IndirectInvokerFunc9::.ctor(native int)
    IL_0006:  ret
  } // end of method IndirectInvokerFunc9::Create

  .method public static !!T11  Invoke<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11>(native int target,
                                                                          !!T1 p1,
                                                                          !!T2 p2,
                                                                          !!T3 p3,
                                                                          !!T4 p4,
                                                                          !!T5 p5,
                                                                          !!T6 p6,
                                                                          !!T7 p7,
                                                                          !!T8 p8,
                                                                          !!T9 p9,
                                                                          !!T10 p10) cil managed
  {
    // Code size       67 (0x43)
    .maxstack  11
    IL_0000:  ldarg      p1
    IL_0004:  nop
    IL_0005:  nop
    IL_0006:  ldarg      p2
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p3
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg      p4
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  ldarg      p5
    IL_001c:  nop
    IL_001d:  nop
    IL_001e:  ldarg      p6
    IL_0022:  nop
    IL_0023:  nop
    IL_0024:  ldarg      p7
    IL_0028:  nop
    IL_0029:  nop
    IL_002a:  ldarg      p8
    IL_002e:  nop
    IL_002f:  nop
    IL_0030:  ldarg      p9
    IL_0034:  nop
    IL_0035:  nop
    IL_0036:  ldarg      p10
    IL_003a:  nop
    IL_003b:  nop
    IL_003c:  ldarg.0
    IL_003d:  calli      !!T11(!!T1,!!T2,!!T3,!!T4,!!T5,!!T6,!!T7,!!T8,!!T9,!!T10)
    IL_0042:  ret
  } // end of method IndirectInvokerFunc9::Invoke

  .method public instance !!T11  CarryingInvoke<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11>(!!T1 p1,
                                                                                    !!T2 p2,
                                                                                    !!T3 p3,
                                                                                    !!T4 p4,
                                                                                    !!T5 p5,
                                                                                    !!T6 p6,
                                                                                    !!T7 p7,
                                                                                    !!T8 p8,
                                                                                    !!T9 p9,
                                                                                    !!T10 p10) cil managed
  {
    // Code size       72 (0x48)
    .maxstack  11
    IL_0000:  ldarg.0
    IL_0001:  ldfld      native int CallMethodPointer.IndirectInvokerFunc9::methodPointer
    IL_0006:  ldarg      p1
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ldarg      p2
    IL_0010:  nop
    IL_0011:  nop
    IL_0012:  ldarg      p3
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  ldarg      p4
    IL_001c:  nop
    IL_001d:  nop
    IL_001e:  ldarg      p5
    IL_0022:  nop
    IL_0023:  nop
    IL_0024:  ldarg      p6
    IL_0028:  nop
    IL_0029:  nop
    IL_002a:  ldarg      p7
    IL_002e:  nop
    IL_002f:  nop
    IL_0030:  ldarg      p8
    IL_0034:  nop
    IL_0035:  nop
    IL_0036:  ldarg      p9
    IL_003a:  nop
    IL_003b:  nop
    IL_003c:  ldarg      p10
    IL_0040:  nop
    IL_0041:  nop
    IL_0042:  call       !!10 CallMethodPointer.IndirectInvokerFunc9::Invoke<!!0,!!1,!!2,!!3,!!4,!!5,!!6,!!7,!!8,!!9,!!10>(native int,
                                                                                                                           !!0,
                                                                                                                           !!1,
                                                                                                                           !!2,
                                                                                                                           !!3,
                                                                                                                           !!4,
                                                                                                                           !!5,
                                                                                                                           !!6,
                                                                                                                           !!7,
                                                                                                                           !!8,
                                                                                                                           !!9)
    IL_0047:  ret
  } // end of method IndirectInvokerFunc9::CarryingInvoke

} // end of class CallMethodPointer.IndirectInvokerFunc9


// =============================================================

// *********** DISASSEMBLY COMPLETE ***********************
