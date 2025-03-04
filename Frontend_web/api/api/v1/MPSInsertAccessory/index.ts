/* eslint-disable */
import type { DefineMethods } from 'aspida'
import type { ReadStream } from 'fs'
import type * as Types from '../../../@types'

export type Methods = DefineMethods<{
  post: {
    query?:
      | {
          Description?: string | null | undefined
          Name?: string | undefined
          Price?: number | undefined
          Quantity?: number | undefined
          Discount?: number | null | undefined
          ShortDescription?: string | null | undefined
          CategoryId?: string | null | undefined
          IsOnlyValidation?: boolean | undefined
        }
      | undefined

    status: 200
    resBody: Types.MPSInsertAccessoryResponse
    reqFormat: FormData

    reqBody: {
      Images?: (File | ReadStream)[] | null | undefined
    }
  }
}>
