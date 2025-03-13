/* eslint-disable */
import type { DefineMethods } from 'aspida';
import type { ReadStream } from 'fs';
import type * as Types from '../../../@types';

export type Methods = DefineMethods<{
  post: {
    query?: {
      CodeAccessory?: string | undefined;
    } | undefined;

    status: 200;
    resBody: Types.MSPInsertImageAccessoryResponse;
    reqFormat: FormData;

    reqBody: {
      Images?: (File | ReadStream)[] | null | undefined;
    };
  };
}>;
