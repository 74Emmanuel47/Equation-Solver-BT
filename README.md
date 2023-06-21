# Equation-Solver-BT
There are many ways to solve equations using programming languages, however in this repository you can find one of the most interesting ways to solve them, BINARY TREES.

Usually, in math problems you have to solve equations with the next form:
        a * b - c/d + e
With a, b, c, d and e like variables.

To solve this equation, we are going to make a binary tree, in which each root node has just two child nodes. A binary tree looks like:

       (A)        ->  Root node  or dad node.
     /     \
    B       C     -> Leaf nodes or child nodes.

We can guess dad node (A) is a symbol that indicates a math operation, and child nodes (B and C) like numbers. Substituting the letters for random values, we can get the next tree:

        *
      /   \
    4       3

    

First, we need to split the equation in several small operations (just a math operation with two terms), but to do it, we need to know about operations hierarchy. Of this way, our example has the next small operations:
        1.- a * b    ->    F
        2.- c / d    ->    G
We substitute F & G for those respective operations, we are going to get the next equation:
        F - G + e
        
We repaet the same process until our equation have only a variable (letter).
        1.- F - G    ->    H
We substitute H for the respective operation, getting the next equation:
        H + e

We repaet the same process until our equation have only a variable (letter).
        1.- H + e    ->    I
We substitute H for the respective operation, getting the final equation:
        I    (just a variable or letter in the equation)
        
