export function sleepByPromise(millisecond: number) {
  return new Promise((resolve) => setTimeout(resolve, millisecond))
}
