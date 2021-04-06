const comics = require('./comics.json')

// 1) Map the list of numbers to a list of their square roots: [1, 9, 16, 100]
let numbers = [1, 9, 16, 100];
var sqrt = numbers.map(Math.sqrt)

console.log("\n1) Square Root Map:")
console.log(sqrt)

// 2) Map the list of words so each is wrapped in a <h1> tag: ["Intro", "Requirements", "Analysis", "Implementation", "Conclusion", "Discussion", "References"]
let words = ["Intro", "Requirements", "Analysis", "Implementation", "Conclusion", "Discussion", "References"]
let titels = words.map(w => `<h1>${w}</h1>`)

console.log("\n2) Words in <h1> tag:")
console.log(titels)

// 3)  Use map to uppercase the words (all letters): ["i’m", "yelling", "today"]
let lowerWords = ["i’m", "yelling", "today"]
let uppercased = lowerWords.map(w => w.toUpperCase())

console.log("\n3) Words to uppercase:")
console.log(uppercased)

// 4) Use map to transform words into their lengths: ["I", "have", "looooooong", "words"]
let transWords = ["I", "have", "looooooong", "words"]
let transformed = transWords.map(w => w.length)

console.log("\n4) Words to transform to length:")
console.log(transformed)

// 5) Get the json file comics.json from the course site. Paste it into your browser’s Javascript console. Use map to get all the image urls, and wrap them in img-tags.
let imgwrapped = comics.map(x => `<img src="${x.img}" alt="${x.title}" />`)

console.log("\n5) Comic images wrapped:")
console.log(imgwrapped)

// 6) Use reduce to sum the array of numbers: [1,2,3,4,5]
let numbersToSum = [1, 2, 3, 4, 5]
let sum = numbersToSum.reduce((a, c) => a + c)

console.log("\n6) Sum the array of numbers:")
console.log(sum);

// 7) Use reduce to sum the x-value of the objects in the array: [{x: 1},{x: 2},{x: 3}]
let objects =  [{x: 1},{x: 2},{x: 3}]
let sumObject = objects.reduce((a, c) => ({x: a.x + c.x}))

console.log("\n7) Sum the array of objects x:")
console.log(sumObject);

// 8) Use reduce to flatten an array of arrays: [[1,2],[3,4],[5,6]]
let numberArray =  [[1,2],[3,4],[5,6]]
let flattend = numberArray.reduce((a, c) => ([...a, ...c]))

console.log("\n8) flaten the array of arrays:")
console.log(flattend);

// 9) Use reduce to return an array of the positive numbers: [-3, -1, 2, 4, 5]
let numbersMixed = [-3, -1, 2, 4, 5]
let posNumbers = numbersMixed.reduce((a, c) => ([...a, Math.abs(c)]), [])

console.log("\n9) Possitive numbers array:")
console.log(posNumbers);