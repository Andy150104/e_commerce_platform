/* eslint-disable */
import type { DefineMethods } from 'aspida';
import type { ReadStream } from 'fs';
import type * as Types from '../../../@types';

export type Methods = DefineMethods<{
  post: {
    query?: {
      Name?: string | undefined;
      Description?: string | undefined;
    } | undefined;

    status: 200;
    resBody: Types.AEPSAddExchangeAccessoryResponse;
    reqFormat: FormData;

    reqBody: {
      Images?: (File | ReadStream)[] | null | undefined;
    };
  };
}>;
