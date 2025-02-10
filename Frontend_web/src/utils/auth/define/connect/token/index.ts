/* eslint-disable */
import type { DefineMethods } from 'aspida'

export type Methods = DefineMethods<{
  post: {
    query?:
      | {
          UserNameOrEmail?: string | undefined
          Password?: string | undefined
        }
      | undefined

    status: 200
    resBody: Blob
  }
}>
