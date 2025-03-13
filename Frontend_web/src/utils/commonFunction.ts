import { Gender, SortBy } from '@PKG_SRC/types/enums/constantBackend'
import type { selectItem } from '@PKG_SRC/types/enums/constantFrontend'

export function sleepByPromise(millisecond: number) {
  return new Promise((resolve) => setTimeout(resolve, millisecond))
}
export function splitText(text: string) {
  return text.split(' ')
}

export function convertImagesToUrls(cartItem: any): string[] {
  return cartItem?.images?.map((image: any) => image.imageUrl) || []
}

export function ConvertCastValue<T>(input: { [key: string]: any }, schemaType: T): T {
  const result: any = {}

  for (const key in schemaType) {
    const expectedType = typeof (schemaType as any)[key]
    if (castMap[expectedType]) {
      result[key] = castMap[expectedType](input[key])
    } else {
      result[key] = input[key]
    }
  }

  return result as T
}
const castMap: CastMap = {
  number: (value: any) => Number(value),
  bigint: (value: any) => BigInt(value),
  string: (value: any) => (value === null || value === '' || value === undefined ? undefined : String(value)),
  boolean: (value: any) => Boolean(value),
  symbol: (value: any) => Symbol(value),
  undefined: (value: any) => value,
  object: (value: any) => value,
  function: (value: any) => value,
}
type CastMap = {
  number: (value: any) => number
  bigint: (value: any) => bigint
  string: (value: any) => string | undefined
  boolean: (value: any) => boolean
  symbol: (value: any) => symbol
  undefined: (value: any) => undefined
  object: (value: any) => object
  function: (value: any) => Function
}

export function createErrorFields<T extends object>(fieldsInitialize: T): Record<keyof T, string> {
  const result: Record<string, string> = {}

  for (const key in fieldsInitialize) {
    if (Object.hasOwnProperty.call(fieldsInitialize, key)) {
      result[key] = ''
    }
  }

  return result as Record<keyof T, string>
}
export type MasterName = 'Gender' | 'SortBy'

export async function getSelectComponentData(masterName: MasterName, _params: any) {
  switch (masterName) {
    case 'Gender':
      return getEnums(Gender)
    case 'SortBy':
      return getEnumsNumber(SortBy)
  }
}

function getEnumsNumber(enumType: { [key: number]: string }) {
  return Object.entries(enumType)
    .filter(([key]) => isNaN(Number(key)))
    .map(([key, value]) => ({
      label: key,
      value: value,
    }))
}

function getEnums(enumType: { [key: string]: string }) {
  const enumItems: Array<selectItem> = []
  Object.entries(enumType).forEach(([key, value]) => {
    const enumItem: selectItem = { value: '', text: '' }
    enumItem.text = key
    enumItem.value = value
    enumItems.push(enumItem)
  })

  return enumItems.map((option) => ({
    label: option.text.replace(/^_/, ''),
    value: option.value,
  }))
}

export function formatPhoneNumber(phoneNumber: string): string | null {
  const cleanedNumber = phoneNumber.replace(/\D/g, '')
  if (!/^0\d{9}$/.test(cleanedNumber)) {
    return null
  }
  return '+84' + cleanedNumber.slice(1)
}
// utils/base64ToFile.ts
export function base64ToFile(base64String: string, filename: string): File {
  const arr = base64String.split(',')
  if (arr.length !== 2) {
    throw new Error('Chuỗi base64 không đúng định dạng (thiếu dấu phẩy).')
  }

  const mimeMatch = arr[0].match(/:(.*?);/)
  if (!mimeMatch) {
    throw new Error('Không tìm thấy MIME type trong base64.')
  }
  const mime = mimeMatch[1] // "image/png" chẳng hạn

  const binaryStr = atob(arr[1])
  const len = binaryStr.length
  const u8arr = new Uint8Array(len)
  for (let i = 0; i < len; i++) {
    u8arr[i] = binaryStr.charCodeAt(i)
  }

  return new File([u8arr], filename, { type: mime })
}
