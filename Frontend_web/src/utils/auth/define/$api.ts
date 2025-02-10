import type { AspidaClient, BasicHeaders } from 'aspida'
import { dataToURLString } from 'aspida'
import type { Methods as Methods_io89wa } from './GoogleResponse'
import type { Methods as Methods_cul8ag } from './LoginWithGoogle'
import type { Methods as Methods_1o52n6h } from './api/v1/UserInsert'
import type { Methods as Methods_1if71ls } from './api/v1/UserInsertVerify'
import type { Methods as Methods_2j4tk2 } from './connect/token'

const api = <T>({ baseURL, fetch }: AspidaClient<T>) => {
  const prefix = (baseURL === undefined ? 'https://localhost:7021' : baseURL).replace(/\/$/, '')
  const PATH0 = '/GoogleResponse'
  const PATH1 = '/LoginWithGoogle'
  const PATH2 = '/api/v1/UserInsert'
  const PATH3 = '/api/v1/UserInsertVerify'
  const PATH4 = '/connect/token'
  const GET = 'GET'
  const POST = 'POST'

  return {
    GoogleResponse: {
      get: (option?: { config?: T | undefined } | undefined) =>
        fetch<Methods_io89wa['get']['resBody'], BasicHeaders, Methods_io89wa['get']['status']>(prefix, PATH0, GET, option).blob(),
      $get: (option?: { config?: T | undefined } | undefined) =>
        fetch<Methods_io89wa['get']['resBody'], BasicHeaders, Methods_io89wa['get']['status']>(prefix, PATH0, GET, option)
          .blob()
          .then((r) => r.body),
      $path: () => `${prefix}${PATH0}`,
    },
    LoginWithGoogle: {
      get: (option?: { config?: T | undefined } | undefined) =>
        fetch<Methods_cul8ag['get']['resBody'], BasicHeaders, Methods_cul8ag['get']['status']>(prefix, PATH1, GET, option).blob(),
      $get: (option?: { config?: T | undefined } | undefined) =>
        fetch<Methods_cul8ag['get']['resBody'], BasicHeaders, Methods_cul8ag['get']['status']>(prefix, PATH1, GET, option)
          .blob()
          .then((r) => r.body),
      $path: () => `${prefix}${PATH1}`,
    },
    api: {
      v1: {
        UserInsert: {
          post: (option: { body: Methods_1o52n6h['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_1o52n6h['post']['resBody'], BasicHeaders, Methods_1o52n6h['post']['status']>(prefix, PATH2, POST, option).json(),
          $post: (option: { body: Methods_1o52n6h['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_1o52n6h['post']['resBody'], BasicHeaders, Methods_1o52n6h['post']['status']>(prefix, PATH2, POST, option)
              .json()
              .then((r) => r.body),
          $path: () => `${prefix}${PATH2}`,
        },
        UserInsertVerify: {
          post: (option: { body: Methods_1if71ls['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_1if71ls['post']['resBody'], BasicHeaders, Methods_1if71ls['post']['status']>(prefix, PATH3, POST, option).json(),
          $post: (option: { body: Methods_1if71ls['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_1if71ls['post']['resBody'], BasicHeaders, Methods_1if71ls['post']['status']>(prefix, PATH3, POST, option)
              .json()
              .then((r) => r.body),
          $path: () => `${prefix}${PATH3}`,
        },
      },
    },
    connect: {
      token: {
        post: (option?: { query?: Methods_2j4tk2['post']['query'] | undefined; config?: T | undefined } | undefined) =>
          fetch<Methods_2j4tk2['post']['resBody'], BasicHeaders, Methods_2j4tk2['post']['status']>(prefix, PATH4, POST, option).blob(),
        $post: (option?: { query?: Methods_2j4tk2['post']['query'] | undefined; config?: T | undefined } | undefined) =>
          fetch<Methods_2j4tk2['post']['resBody'], BasicHeaders, Methods_2j4tk2['post']['status']>(prefix, PATH4, POST, option)
            .blob()
            .then((r) => r.body),
        $path: (option?: { method: 'post'; query: Methods_2j4tk2['post']['query'] } | undefined) =>
          `${prefix}${PATH4}${option && option.query ? `?${dataToURLString(option.query)}` : ''}`,
      },
    },
  }
}

export type ApiInstance = ReturnType<typeof api>
export default api
