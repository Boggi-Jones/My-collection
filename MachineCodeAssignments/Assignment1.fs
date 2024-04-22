// T-501-FMAL, Spring 2022, Assignment 1

(*
STUDENT NAMES HERE:
  Group 29:
  
  Ísak Snær Sigmarsson
  Skarphéðinn Ísak Sigurðsson
*)

module Assignment1

  let rec findremove x ys =
      match ys with
      | [] -> 0, []
      | (x', v) :: ys' -> if x = x' then
                              v, ys'
                          else
                              let w, zs = findremove x ys' 
                              w, (x', v) :: zs

  let rec mults xs =
      match xs with
      | [] -> []
      | x :: xs' -> let v, ys = findremove x (mults xs')
                    (x, v+v) :: ys

   void f (int *p) {
     int i;  
     i = *p; 
     print (i); 
     i = 3;  
   }
 
   void main () {
     int i;
     i = 13;
     f (&i); 
     print i;
   } 


////////////////////////////////////////////////////////////////////////
// Problem 1                                                          //
////////////////////////////////////////////////////////////////////////

// nf : int -> int
let rec nf n =
  if n < 1 then 0
  else if n = 1 then 1
  else nf (n - 2) + n



////////////////////////////////////////////////////////////////////////
// Problem 2                                                          //
////////////////////////////////////////////////////////////////////////

// truesAndLength : bool list -> int * int
let rec truesAndLength bs =
  match bs with
  | [] -> (0, 0)
  | b::bs -> 
    match truesAndLength bs with
    | (_,_) -> if b = true then fst (truesAndLength bs) + 1, snd (truesAndLength bs) + 1
                   else fst (truesAndLength bs) + 1, snd (truesAndLength bs)


// majority : bool list -> bool
let rec majority bs =
  match bs with
  | [] -> false
  | bs ->
    match truesAndLength bs with
    | (_,_) -> if (snd (truesAndLength bs)) >= ((fst (truesAndLength bs)) / 2) then true
               else false

// majority2 : ('a -> bool) -> 'a list -> bool
let majority2 p xs = majority (List.map p xs)

// majorityLarge : int list -> bool
let majorityLarge xs = majority2 (fun x -> x >= 100) xs



////////////////////////////////////////////////////////////////////////
// Problem 3                                                          //
////////////////////////////////////////////////////////////////////////

// isGood : ('a * 'b) list -> bool when 'a: equality
let rec isGood ps = 
  match ps with
  | [] -> true
  | [_] -> true
  | p::(p'::_ as ps) -> 
    let a = fst p
    let b = fst p'
    if a = b then false
    else isGood ps

// makeGoodInt : ('a * int) list -> ('a * int) list when 'a: equality
let rec makeGoodInt ps =
  match ps with
  | [] -> []
  | [_] -> ps
  | p :: ps ->
    let a = fst p
    if a = fst ps.[0] then (a, snd p + snd ps.[0]) :: makeGoodInt ps.[1..]
    else p :: makeGoodInt ps

// makeGoodWith : ('b -> 'b -> 'b) -> ('a * 'b) list -> ('a * 'b) list
//                                                     when 'a: equality

let rec makeGoodWith f ps =
  match ps with
  | [] -> []
  | [_] -> ps
  | p :: ps ->
    let a = fst p
    if a = fst ps.[0] then (a, f (snd p) (snd ps.[0])) :: makeGoodWith f ps.[1..]
    else p :: makeGoodWith f ps


////////////////////////////////////////////////////////////////////////
// Problem 4                                                          //
////////////////////////////////////////////////////////////////////////

// shuffle : 'a list -> 'a list
let rec shuffle xs =
  match xs with
  | [] -> []
  | [x] -> [x]
  | x::y::xs -> x :: (shuffle xs @ [y])

(*
ANSWER 4(i) HERE:
  This fuction is quadratic mainly due to the way cocatenation works with the @ symbol
  in F#. This shuffle function goes to the bottom of the list recursively, giving us O(n)
  time complexity, but as it is going back it adds y at the end of the list, meaning it traverses
  the list each time. This gives us O(n^2) time complexity.
*)


// shuffle2 : 'a list -> 'a list
let shuffle2 xs =
  // shuffleAcc : 'a list -> 'a list -> 'a list
  let rec shuffleAcc acc xs =
    match xs with
    | [] -> acc
    | [x] -> x::acc
    | x::y::xs' -> x :: (shuffleAcc (y::acc) xs')
  in shuffleAcc [] xs



////////////////////////////////////////////////////////////////////////
// Problem 5                                                          //
////////////////////////////////////////////////////////////////////////

exception FooException

// foo : int list -> int
let rec foo xs =
  match xs with
  | [] -> 0
  | x::xs -> if x < 0 then raise FooException else x + foo xs

// foo2 : int list -> int option
let rec foo2 xs =
  match xs with
  | [] -> Some (0)
  | x::xs -> if x < 0 then None else Some(x + (foo2 xs).Value)

// fooDefault : int -> int list -> int
let fooDefault d xs = try foo xs with FooException -> d

// foo2Default : int -> int list -> int
let foo2Default d xs = if (foo2 xs) = None then d
                       else (foo2 xs).Value



////////////////////////////////////////////////////////////////////////
// Problem 6                                                          //
////////////////////////////////////////////////////////////////////////

type mtree =
  | Leaf                            // leaf
  | Branch of int * mtree * mtree   // branch (value, left, right)
  | Mul of int * mtree              // multiply values below by given int

type 'a tree =
  | Lf                              // leaf
  | Br of 'a * 'a tree * 'a tree    // branch (value, left, right)

type pos =
  | S                               // the root ("stop") position
  | L of pos                        // a position in the left subtree
  | R of pos                        // a position in the right subtree

// getValAt : pos -> mtree -> int
let rec getValAt p t =
  match p, t with
  | S, Leaf -> 0
  | S, Branch(x, _, _) -> x
  | L p, Leaf -> failwith "Position does not exist"
  | L p, Branch (_, t1, _) -> getValAt p t1
  | R p, Leaf -> failwith "Position does not exist"
  | R p, Branch (_, _, t2) -> getValAt p t2
  | _, Mul (q, t) -> q * getValAt p t

// toTree : mtree -> int tree
let toTree t =
  let rec toTreeHelp t acc =
    match t with
    | Leaf -> Lf
    | Branch (x, t1, t2) -> Br ((x * acc), toTreeHelp t1 acc, toTreeHelp t2 acc)
    | Mul (q, t) -> toTreeHelp t (q * acc)
  in toTreeHelp t 1





