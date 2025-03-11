/* eslint-disable */
import type { DefineMethods } from 'aspida'
import type * as Types from '../../../@types'

export type Methods = DefineMethods<{
  get: {
    query?:
      | {
          /** 2: show wards */
          depth?: number | undefined
        }
      | undefined

    status: 200
    /** Successful Response */
    resBody: Types.District
  }
}>
