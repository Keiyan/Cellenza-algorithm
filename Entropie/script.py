import math

f = open('IntegerArray.txt', "r")
numbers = [int(x) for x in f.readlines()]

total = len(numbers)

num_counts = [0] * max(numbers)

for n in numbers:
    num_counts[n - 1] += 1

if max(num_counts) == 1
    print("l'entropie est de n")

entropy = 0

for count in num_counts:
    # If no bytes of this value were seen in the value, it doesn't affect
    # the entropy of the file.
    if count == 0:
        continue
    # p is the probability of seeing this byte in the file, as a floating-
    # point number
    p = 1.0 * count / total
    entropy -= p * math.log(p, total)

print(entropy)