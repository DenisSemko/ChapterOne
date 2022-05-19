export interface Combination {
    id: string,
    reader: string,
    author: string,
    year: number,
    genre: string,
    publisher: string,
    shortDescription: string,
    tempCombination: string,
    resultPercentage: number,
    isSuccessful?: boolean
}