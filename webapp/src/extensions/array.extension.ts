export {}

if (!Array.prototype.partition) {
  // eslint-disable-next-line
  Array.prototype.partition = function <T>(
    this: T[],
    predicate: (element: T) => boolean
  ): { first: T[]; second: T[] } {
    const first: T[] = []
    const second: T[] = []
    for (const element of this) {
      ;(predicate(element) ? first : second).push(element)
    }
    return { first, second }
  }
}

if (!Array.prototype.distinct) {
  // eslint-disable-next-line
  Array.prototype.distinct = function <T>(this: T[]): T[] {
    return Array.from(new Set(this))
  }
}

declare global {
  // tslint:disable-next-line: interface-name
  interface Array<T> {
    /**
     * Splits the original array into two lists, where the first list contains elements for which the predicate yielded true, while the second list contains elements for which the predicate yielded false.
     * @param predicate
     */
    partition(predicate: (element: T) => boolean): { first: T[]; second: T[] }

    /**
     * Returns distinct elements from an array.
     */
    distinct(): T[]
  }
}
