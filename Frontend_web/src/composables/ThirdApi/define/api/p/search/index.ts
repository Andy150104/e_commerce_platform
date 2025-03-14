/* eslint-disable */
import type { DefineMethods } from 'aspida'
import type * as Types from '../../../@types'

export type Methods = DefineMethods<{
  get: {
    query: {
      /** Follow [lunr](https://lunr.readthedocs.io/en/latest/usage.html#using-query-strings) syntax. */
      q: string
    }

    status: 200
    /** Successful Response */
    resBody: Types.SearchResult[]
  }
}>
