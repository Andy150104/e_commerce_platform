import { Gender } from '@PKG_SRC/types/enums/constantBackend'
import type { selectItem } from '@PKG_SRC/types/enums/constantFrontend'

export function sleepByPromise(millisecond: number) {
  return new Promise((resolve) => setTimeout(resolve, millisecond))
}
export function splitText(text: string) {
  return text.split(' ')
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
export type MasterName = 'Gender'

export async function getSelectComponentData(masterName: MasterName, _params: any) {
  switch (masterName) {
    case 'Gender':
      return getEnums(Gender)
  }
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