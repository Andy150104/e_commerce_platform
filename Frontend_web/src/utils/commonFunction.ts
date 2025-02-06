export function sleepByPromise(millisecond: number) {
  return new Promise((resolve) => setTimeout(resolve, millisecond))
}
export function splitText(text: string){
  return text.split(' ')
}