// T-501-FMAL, Spring 2022, Assignment 4

(*
STUDENT NAMES HERE:

Group 16

Ísak Snær Sigmarsson
Skarphéðinn Ísak Sigurðsson


*)

module Assignment4

// (You can ignore this line, it stops F# from printing some messages
// about references in some cases.)
#nowarn "3370";;

////////////////////////////////////////////////////////////////////////
// Problem 1                                                          //
////////////////////////////////////////////////////////////////////////

(* ANSWER 1 HERE:
     (i)   (\f. (\x. f (f x))) (\y. y) z
           =>
           (\x. (\y. y) (f x)) z
           => 
           (\y. y) (f x)
           =>
           f x

    (ii)   (\g. g z) (\z. z) z
           =>
           ((\z. z) z)z
           =>
           z z

   (iii)   (\h. h (\k. k z)) (\y. y y) z
           =>
           (\y. y y) (\k. k z) z
           =>
           (\k. k z) (\k. k z) z
           =>
           k (\k. k z) z
           =>
           k z z
          
*)



////////////////////////////////////////////////////////////////////////
// Problem 2                                                          //
////////////////////////////////////////////////////////////////////////

(* ANSWER 2 HERE:
    (i) t1 = f (x)
        t2 = y (x)
        t3 = x
        t4 = x (y)
        t5 = f (x (y))

   (ii) t6 = x (y (x))
*)



////////////////////////////////////////////////////////////////////////
// Problem 3                                                          //
////////////////////////////////////////////////////////////////////////

// The standard node-labelled tree datatype
type 'a tree = Lf | Br of 'a * 'a tree * 'a tree

// A mutable node-labelled tree datatype
type 'a refTree = RLf | RBr of 'a * 'a refTree ref * 'a refTree ref

// Convert a standard tree into a mutable tree
// makeRefTree : 'a tree -> 'a refTree ref
let rec makeRefTree t =
  match t with
  | Lf -> ref RLf
  | Br (x, l, r) -> ref (RBr (x, makeRefTree l, makeRefTree r))

// Convert a mutable tree into a standard tree
// freeze : 'a refTree ref -> 'a tree
let rec freeze tref =
  match !tref with
  | RLf -> Lf
  | RBr (x, lref, rref) -> Br (x, freeze lref, freeze rref)

// Swap the contents of two references
// swap : 'a ref -> 'a ref -> unit
let swap r1 r2 =
  let x = !r1
  r1 := !r2;
  r2 := x

// Swap the left and right branches of each node, recursively
// mirror : 'a refTree ref -> unit
let rec mirror tref =
  match !tref with
  | RLf -> ()
  | RBr (x, lref, rref) ->
    swap lref rref; mirror lref; mirror rref
    
let testMirror (t : int tree) =
  let tref = makeRefTree t
  mirror tref;
  freeze tref

// Do a single rotation (if possible)
// rotate : 'a refTree ref -> unit
let rotate tref =
    match !tref with
    | RLf -> ()
    | RBr (x, lref, rref) ->
        match !lref with
        | RLf -> ()
        | RBr (y, llref, lrref) ->
          let pivot = !lref
          lref := !lrref;
          lrref := !tref;
          tref := pivot


let testRotate (t : int tree) =
  let tref = makeRefTree t
  rotate tref;
  freeze tref

////////////////////////////////////////////////////////////////////////
// Problem 4                                                          //
////////////////////////////////////////////////////////////////////////

type expr =
    | Num of int
    | Var of string
    | Plus of expr * expr
type stmt =
    | Assign of string * expr
    | Block of string * stmt list // Block (x, stmts) is a block that
                                  // declares the variable x
    | If of expr * stmt * stmt
    | While of expr * stmt
    | Print of expr

type naivestore = Map<string,int>
let emptystore : Map<string,int> = Map.empty

// (getSto store x) gets the value of the variable x from the store.
// If x is not in the store (for example, because it has not been
// declared yet), then getSto returns 0.
let getSto (store : naivestore) x = if store.ContainsKey x then store.Item x else 0

// (setSto store (k, v)) returns a new store, in which the value of the
// variable k is set to v
let setSto (store : naivestore) (k, v) = store.Add(k, v)

let rec eval e (store : naivestore) : int =
    match e with
    | Num i -> i
    | Var x -> getSto store x
    | Plus(e1, e2) -> eval e1 store + eval e2 store

let rec exec stmt (store : naivestore) : naivestore =
    match stmt with
    | Assign (x, e) -> setSto store (x, eval e store)
    | If (e1, stmt1, stmt2) ->
        if eval e1 store <> 0 then exec stmt1 store else exec stmt2 store
    | Block (x, stmts) ->
        let old_sto = store
        let store = setSto store (x, 0)
        let rec loop ss sto =
            match ss with
            | []     -> sto
            | s1::sr -> 
              loop sr (exec s1 sto)
        loop stmts store;
        setSto store (x, getSto old_sto x)
    | While (e, stmt) ->
        let rec loop sto =
            if eval e sto = 0 then sto
                              else loop (exec stmt sto)
        loop store
    | Print e -> printf "%d\n" (eval e store); store

let run stmt = exec stmt emptystore |> ignore

// The example program, can be executed with (run test)
let test =
  Block("x",
    [ Assign ("x", Num 1)
    ; Print (Var "x")
    ; Block ("x",
      [ Print (Var "x")
      ; Assign ("x", Num 2)
      ; Print (Var "x")
      ])
    ; Print (Var "x")
    ])



////////////////////////////////////////////////////////////////////////
// Problem 5                                                          //
////////////////////////////////////////////////////////////////////////

(* ANSWER 5 HERE:

  void *f(int **p, int *q) {
    int *r = *p;                       pointer *r points to the contents of *p
    if (*r == 0) {                     *r is not 0 for h(1)
      *r = *q;
    }
    else {
      *p = q;                          *p now points at q (temporary context)
    }
  }

  void g(int *t, int x, int y) {
    f(&t, &x);                          &t = 1, &x = 100          &t = 0,
    print *t;                           *t = 100                  &t = 100
    print x;                             x = 100                  x = 100
    print y;                             y = 101                  y = 101
    f(&t, &y);                          &t = 100, &y = 101        &t = 100, &y = 101
    print *t;                           *t = 101                  *t = 101             
    print x;                             x = 100                   x = 100
    print y;                             y = 101                   y = 101
  }

  void h(int a) {
  g(&a, 100, 101);                     &a = 1                     &a = 0
  print a;                              a = 1                      a = 100
  }


    (i) h(1) prints:
        100
        100
        101
        101
        100
        101
        1
    
   (ii) h(0) prints:
        100
        100
        101
        101
        100
        101
        100

*)

