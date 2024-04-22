// T-501-FMAL, Spring 2022, Assignment 3

(*
STUDENT NAMES HERE:

Group 11

Ísak Snær Sigmarsson
Skarphéðinn Ísak Sigurðsson

*)

module Assignment3

// (You can ignore this line, it stops F# from printing some messages
// about references in some cases.)
#nowarn "3370";;

////////////////////////////////////////////////////////////////////////
// Problem 1                                                          //
////////////////////////////////////////////////////////////////////////

let x = 10 in
    let f y = (x + 1, x + y) in
        let x = fst (f 20) in
            let g y = (x + 2, x + y) in
                let x = fst (f 30) in
                    snd (g 40)

(* ANSWER 1 HERE:
   (i)

    In the static scope the expressionn above evaluates to 51.

    x is originally declared with the value 10.

    The function f y returns the pair (x + 1, x + y), and applies x + y to y.

    f is declared after the definition of x, and is called immeadiately after its decleration,
    applying the result of the first element of the pair it returns to x.

    At this point x = 11.

    The function g is declared in the next step, and since it is declared before any additional
    changes can occur for x, then in the static scope x still retains the value of 11.

    This would mean that calling: let x = fst (f 30) would have no effect on the value of x when the
    last bit of the expression is called, x would still have the value 11.

    When we then call snd (g 40) at the end of the expression, it would evaluate to 51, since the second element
    of the pair in g 40 would be 11 + 40.

  (ii)

    In the dynamic scope the expression above would evaluate to 52.

    As in the static case, x originally has the value of 10 and in the function call

    let f y = (x + 1, x + y) in
        let x = fst (f 20) in
    
    then x = 11

    However unlike the static scope the value of x will change after the decleration of the function g, so in this following
    portion of the expression:

    let g y = (x + 2, x + y) in
        let x = fst (f 30) in
    
    then x would be changed due to the changes applied to it by the function f

    x = 12

    So when the function snd (g 40) is called, the second element of the pair would evaluate to 12 + 40 = 52.

    So the expression evaluates to 52.

*)



////////////////////////////////////////////////////////////////////////
// Problem 2                                                          //
////////////////////////////////////////////////////////////////////////

// fun1: ’a -> (’a -> ’b) -> ’b
// type of x: 'a
// type of k: ('a -> 'b)

// type of k x: b'

let fun1 x k = k x 

// fun2: (’a -> ’b) -> ((’a -> ’c) -> ’d) -> (’b -> ’c) -> ’d
// type of f: (’a -> ’b)
// type of t: ((’a -> ’c) -> ’d)
// type of k: (’b -> ’c)

// type of f k:

let fun2 f t k = t (f >> k)

// fun3: (’a -> ’b -> ’c) -> ’a * ’b -> ’c
// type of f: (’a -> ’b -> ’c)
// type of x: ’a
// type of y: ’b
// type of (x, y): ’a * ’b
let fun3 f (x, y) = f x y

// fun4: (’a -> ’b -> ’a) -> ’a * ’b -> ’a
// type of f: (’a -> ’b -> ’a)
// type of x: ’a
// type of y: ’b
// type of (x, y): ’a * ’b

// type of f x: ’b -> ’a

let fun4 f (x, y) = f (f x y) y

// fun5: (’a -> ’a -> ’a) -> ’a * ’a -> ’a
// type of f: (’a -> ’a -> ’a)
// type of x: ’a
// type of y: ’a
// type of (x, y): ’a * ’a

let fun5 f (x, y) = f y (f x y)

////////////////////////////////////////////////////////////////////////
// Problem 3                                                          //
////////////////////////////////////////////////////////////////////////

(* ANSWER 3 HERE:
     (i) ’a * ’a and bool * (int * int)

    In this instance we are trying to unify the components 'a * 'a with bool * (int * int) which are the types boolean and a pair of integers.

    This unification is not possible however, since for the first component to be unified we would have to make
    'a → bool, and then for the second component we would have to unify a' again, which now has the type boolean, and a pair of integers, 
    which is not possible since a boolean cannot be unified with a pair of integers, since the types do not match.

    Therefor this unification fails.
    
    (ii) ’a * ’b and bool * (’a * int) -> Spyrja TA

    In this instance we are trying to unify 'a * 'b with bool * ('a * int), which are the types boolean and a pair containing the type of 'a and int.

    This unification should be possible, since we begin by unifying 'a with bool, so from that point 'a has type bool. For the second component we unify 'b with the pair ('a * int), this should be possible
    since the type of 'a in this case is bool and we would thus end with 'b = (bool * int). So we substitute a' with bool and b' with ('a * int), which would now become (bool * int).

    This unification is possible with these substitutions.

   (iii) ’a * ’b and ’a * (’a * int) -> Spyrja TA

    In this instance we are trying to unify 'a * 'b with 'a * ('a * int), which have the unconstrained type 'a and the pair of 'a * int.

    This unification might not be possible however, since in the first component we would unify the 'a with the type 'a which creates an unconstrained type and might lead to an infinite type.
    Unifying 'b with the second component then makes 'b = ('a * int) which is still an unconstrained type.

    This unification fails due to the creation of an infinite type.

    (iv) ’a * ’a and (’b -> ’b) * (’b -> ’b)

    In this instance we are trying to unify 'a * 'a with (’b -> ’b) * (’b -> ’b), which are both function types that take some value 'b and return 'b.

    This unification is possible, since after unifying the first component 'a becomes (’b -> ’b). When the second component is unified 'a is already a similar function type and so the
    unification succeeds.

    This unification is thus possible with these substitutions.

     (v) ’a * ’a and (’b -> ’c) * (int -> ’b)

     In this instance we are trying to unify 'a * 'a with (’b -> ’c) * (int -> ’b), which are both function types that have different parameters and return values.

     This unification might fail, since after unifying the first component we would get 'a = (’b -> ’c). When we then try to unify the second component 'a is already a functional type of a sort
     that does not match (int -> ’b) due to different parameters and return values.

     This unification fails due to differences in function types.
    
    
*)



////////////////////////////////////////////////////////////////////////
// Some type declarations, do not change these                        //
////////////////////////////////////////////////////////////////////////

type expr =
    | Var of string
    | Let of string * expr * expr
    | Call of expr * expr
    | LetFun of string * string * expr * expr
    | Num of int
    | Plus of expr * expr
    | Minus of expr * expr
    | Times of expr * expr
    | Divide of expr * expr
    | Neg of expr
    | True
    | False
    | Equal of expr * expr
    | Less of expr * expr
    | ITE of expr * expr * expr
    | Pair of expr * expr                       // pairing
    | Fst of expr                               // first component
    | Snd of expr                               // second component
type 'a envir = (string * 'a) list

type typ =
    | TVar of typevar
    | Int
    | Bool
    | Fun of typ * typ
    | Prod of typ * typ                         // product type
and typevar = (tvarkind * int) ref
and tvarkind =
    | NoLink of string
    | LinkTo of typ

type typescheme =
    | TypeScheme of typevar list * typ

type value =
    | I of int
    | B of bool
    | F of string * string * expr * value envir
    | P of expr * expr * value envir            // pair closure

////////////////////////////////////////////////////////////////////////
// Some helper functions, do not change these                         //
////////////////////////////////////////////////////////////////////////

let rec lookup (x : string) (env : 'a envir) : 'a =
    match env with
    | []          -> failwith (x + " not found")
    | (y, v)::env -> if x = y then v else lookup x env

let setTvKind (tv : typevar) (kind : tvarkind) : unit =
    let _, lvl = !tv
    tv := kind, lvl

let setTvLevel (tv : typevar) (lvl : int) : unit =
    let kind, _ = !tv
    tv := kind, lvl

let rec normType (t : typ) : typ =
    match t with
    | TVar tv ->
        match !tv with
        | LinkTo t', _ -> let tn = normType t'
                          setTvKind tv (LinkTo tn); tn
        | _ -> t
    |  _ -> t

let rec union xs ys =
    match xs with
    | []    -> ys
    | x::xs -> if List.contains x ys then union xs ys
               else x :: union xs ys

let rec freeTypeVars (t : typ) : typevar list =
    match normType t with
    | TVar tv      -> [tv]
    | Int          -> []
    | Bool         -> []
    | Fun (t1, t2) -> union (freeTypeVars t1) (freeTypeVars t2)
    | Prod (t1, t2) -> union (freeTypeVars t1) (freeTypeVars t2)

let occursCheck (tv : typevar) (tvs : typevar list) : unit =
    if List.contains tv tvs then failwith "type error: circularity"
    else ()

let pruneLevel (maxLevel : int) (tvs : typevar list) : unit =
    let reducelevel tv =
        let _, lvl = !tv
        setTvLevel tv (min lvl maxLevel)
    List.iter reducelevel tvs

let rec linkVarToType (tv : typevar) (t : typ) : unit =
    let _, lvl = !tv
    let tvs = freeTypeVars t
    occursCheck tv tvs;
    pruneLevel lvl tvs;
    setTvKind tv (LinkTo t)

let paren b s = if b then "(" + s + ")" else s

let prettyprintType (t : typ) : string =
    let rec prettyprintType' t acc =
        match normType t with
        | TVar v ->
            match !v with
            | NoLink name, _ -> name
            | _ -> failwith "we should not have ended up here"
        | Int -> "int"
        | Bool -> "bool"
        | Fun (t1, t2) ->
            let s1 = prettyprintType' t1 true
            let s2 = prettyprintType' t2 false
            paren acc (sprintf "%s -> %s" s1 s2)
        | Prod (t1, t2) ->
            let s1 = prettyprintType' t1 true
            let s2 = prettyprintType' t2 true
            paren acc (sprintf "%s * %s" s1 s2)
    prettyprintType' t false

let tyvarno : int ref = ref 0
let newTypeVar (lvl : int) : typevar =
    let rec mkname i res =
            if i < 26 then char(97+i) :: res
            else mkname (i/26-1) (char(97+i%26) :: res)
    let intToName i = new System.String(Array.ofList('\'' :: mkname i []))
    tyvarno := !tyvarno + 1;
    ref (NoLink (intToName (!tyvarno)), lvl)

let rec generalize (lvl : int) (t : typ) : typescheme =
    let notfreeincontext tv =
        let _, linkLvl = !tv
        linkLvl > lvl
    let tvs = List.filter notfreeincontext (freeTypeVars t)
    TypeScheme (tvs, t)

let rec copyType (subst : (typevar * typ) list) (t : typ) : typ =
    match t with
    | TVar tv ->
        let rec loop subst =
            match subst with
            | (tv', t') :: subst -> if tv = tv' then t' else loop subst
            | [] -> match !tv with
                    | NoLink _, _ -> t
                    | LinkTo t', _ -> copyType subst t'
        loop subst
    | Fun (t1,t2) -> Fun (copyType subst t1, copyType subst t2)
    | Int         -> Int
    | Bool        -> Bool
    | Prod (t1, t2) -> Prod (copyType subst t1, copyType subst t2)

let specialize (lvl : int) (TypeScheme (tvs, t)) : typ =
    let bindfresh tv = (tv, TVar (newTypeVar lvl))
    match tvs with
    | [] -> t
    | _  -> let subst = List.map bindfresh tvs
            copyType subst t



////////////////////////////////////////////////////////////////////////
// Problem 4                                                          //
////////////////////////////////////////////////////////////////////////

let rec unify (t1 : typ) (t2 : typ) : unit =
    let t1' = normType t1
    let t2' = normType t2
    match t1', t2' with
    | Int,  Int  -> ()
    | Bool, Bool -> ()
    | Fun (t11, t12), Fun (t21, t22) -> unify t11 t21; unify t12 t22
    | Prod (t11, t12), Prod (t21, t22) -> unify t11 t21; unify t12 t22
    | TVar tv1, TVar tv2 ->
        let _, tv1level = !tv1
        let _, tv2level = !tv2
        if tv1 = tv2                then ()
        else if tv1level < tv2level then linkVarToType tv1 t2'
                                    else linkVarToType tv2 t1'
    | TVar tv1, _ -> linkVarToType tv1 t2'
    | _, TVar tv2 -> linkVarToType tv2 t1'
    | _, _ -> failwith ("cannot unify " + prettyprintType t1' + " and " + prettyprintType t2')

let a = ref (NoLink "'a", 0)
let b = ref (NoLink "'b", 0)
let c = ref (NoLink "'c", 0)
let unifyTest t1 t2 =
  a := (NoLink "'a", 0);
  b := (NoLink "'b", 0);
  c := (NoLink "'c", 0);
  unify t1 t2;
  prettyprintType t1

////////////////////////////////////////////////////////////////////////
// Problem 5                                                          //
////////////////////////////////////////////////////////////////////////

let rec infer (e : expr) (lvl : int) (env : typescheme envir) : typ =
    match e with
    | Var x  -> specialize lvl (lookup x env)
    | Let (x, erhs, ebody) ->
        let lvl' = lvl + 1
        let tx = infer erhs lvl' env
        let env' = (x, generalize lvl tx) :: env
        infer ebody lvl env'
    | Call (efun, earg) ->
        let tf = infer efun lvl env
        let tx = infer earg lvl env
        let tr = TVar (newTypeVar lvl)
        unify tf (Fun (tx, tr)); tr
    | LetFun (f, x, erhs, ebody) ->
        let lvl' = lvl + 1
        let tf = TVar (newTypeVar lvl')
        let tx = TVar (newTypeVar lvl')
        let env' = (x, TypeScheme ([], tx))
                      :: (f, TypeScheme ([], tf)) :: env
        let tr = infer erhs lvl' env'
        let () = unify tf (Fun (tx, tr))
        let env'' = (f, generalize lvl tf) :: env
        infer ebody lvl env''
    | Num i -> Int
    | Plus (e1, e2) ->
        let t1 = infer e1 lvl env
        let t2 = infer e2 lvl env
        unify Int t1; unify Int t2; Int
    | Minus (e1, e2) ->
        let t1 = infer e1 lvl env
        let t2 = infer e2 lvl env
        unify Int t1; unify Int t2; Int
    | Times (e1, e2) ->
        let t1 = infer e1 lvl env
        let t2 = infer e2 lvl env
        unify Int t1; unify Int t2; Int
    | Divide (e1, e2) ->
        let t1 = infer e1 lvl env
        let t2 = infer e2 lvl env
        unify Int t1; unify Int t2; Int
    | Neg e ->
        let t = infer e lvl env
        unify Int t; Int
    | True  -> Bool
    | False -> Bool
    | Equal (e1, e2) ->
        let t1 = infer e1 lvl env
        let t2 = infer e2 lvl env
        unify t1 Int; unify t2 Int;
        Bool
    | Less (e1, e2) ->
        let t1 = infer e1 lvl env
        let t2 = infer e2 lvl env
        unify Int t1; unify Int t2; Bool
    | ITE (e, e1, e2) ->
        let t1 = infer e1 lvl env
        let t2 = infer e2 lvl env
        unify Bool (infer e lvl env); unify t1 t2; t1

    | Pair (e1, e2) ->
        let t1 = infer e1 lvl env
        let t2 = infer e2 lvl env
        Prod (t1, t2)
        
    | Fst e ->
        let te = infer e lvl env
        let tx = TVar (newTypeVar lvl)
        let ty = TVar (newTypeVar lvl)
        unify te (Prod (tx, ty)); tx
    | Snd e ->
        let te = infer e lvl env
        let tx = TVar (newTypeVar lvl)
        let ty = TVar (newTypeVar lvl)
        unify te (Prod (tx, ty)); ty

let inferTop e =
    tyvarno := 0; prettyprintType (infer e 0 [])



////////////////////////////////////////////////////////////////////////
// Problem 6                                                          //
////////////////////////////////////////////////////////////////////////

let rec eval (e : expr) (env : value envir) : value =
    match e with
    | Var x  ->  lookup x env
    | Let (x, erhs, ebody) ->
         let v = eval erhs env
         let env' = (x, v) :: env
         eval ebody env'
    | Call (efun, earg) ->
         let clo = eval efun env
         match clo with
         | F (f, x, ebody, env0) ->
             let v = eval earg env
             let env' = (x, v) :: (f, clo) :: env0
             eval ebody env'
         | _   -> failwith "expression called not a function"
    | LetFun (f, x, erhs, ebody) ->
         let env' = (f, F (f, x, erhs, env)) :: env
         eval ebody env'
    | Num i -> I i
    | Plus  (e1, e2) ->
         match eval e1 env, eval e2 env with
         | I i1, I i2 -> I (i1 + i2)
         | _ -> failwith "argument of + not integers"
    | Minus  (e1, e2) ->
         match eval e1 env, eval e2 env with
         | I i1, I i2 -> I (i1 - i2)
         | _ -> failwith "arguments of - not integers"
    | Times (e1, e2) ->
         match eval e1 env, eval e2 env with
         | I i1, I i2 -> I (i1 * i2)
         | _ -> failwith "arguments of * not integers"
    | Divide (e1, e2) ->
         match eval e1 env, eval e2 env with
         | I i1, I i2 ->
             if i2 = 0 then failwith "division by 0"
             else I (i1 / i2)
         | _ -> failwith "arguments of / not integers"
    | Neg e ->
         match eval e env with
         | I i -> I (- i)
         | _ -> failwith "argument of negation not an integer"
    | True  -> B true
    | False -> B false
    | Equal (e1, e2) ->
         match eval e1 env, eval e2 env with
         | I i1, I i2 -> B (i1 = i2)
         | _ -> failwith "arguments of = not integers"
    | Less (e1, e2) ->
         match eval e1 env, eval e2 env with
         | I i1, I i2 -> B (i1 < i2)
         | _ -> failwith "arguments of < not integers"
    | ITE (e, e1, e2) ->
         match eval e env with
         | B b -> if b then eval e1 env else eval e2 env
         | _ -> failwith "guard of if-then-else not a boolean"

    | Pair (e1, e2) -> P (e1, e2, env) 
    | Fst e ->
        match eval e env with
        | P (e1, e2, env) -> eval e1 env
    | Snd e ->
        match eval e env with
        | P (e1, e2, env) -> eval e2 env



////////////////////////////////////////////////////////////////////////
// Problem 7                                                          //
////////////////////////////////////////////////////////////////////////

(* ANSWER 7 HERE:

    The issue has to do with static typing and dynamic typing of functional languages.

    A functional language that is dynamically typed would certainly prevent some ill typed code from running, such as adding a function to an integer:

        let fun x = x + 1 in f + 4
    
    This would be considered ill typed in the dynamic scope, but changing the function like so:

        let fun x = x + 1 in 1 = 1 then 7 else f + 4
    
    Would make it so that the function gets evaluated since we never go into the else case of the function itself.

    This is similar in the case of the evaluation of an expression such as: let p = (0, snd 1) in fst p

    In the dynamic scope, examining the result of fst p would mean that we dont need to evaluate snd 1, since thats not the component we are interested in.
    As such the code will evaluate to be correct even though it is ill typed.

    The reason this is not always the case, i.e. ill typed code that might have been evaluated to be correct in previous instances might not be evaluated correctly in others, is
    due to how code is evaluated in the static scope.

    Most programming languages are statically typed, and languages that are statically typed evaluate code at compile-time instead of at run-time. This means that even though we might
    just be interested in the results of certain components of our ill typed code, in statically typed languages all our code will be evaluated at compile-time to sift out logic errors and bugs
    early on in the development process, since fixing such things is better done in that process rather than after the code has been shipped.

    So in the static scope, the function:

        let fun x = x + 1 in 1 = 1 then 7 else f + 4
    
    And:

        let p = (0, snd 1) in fst p
    
    Would be considered ill typed since all of the code is evaluated even though some components within it might never be used. We cannot force a statically typed language to ignore such things.
*)



