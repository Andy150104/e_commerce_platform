/* eslint-disable */
import type { DefineMethods } from 'aspida';
import type { ReadStream } from 'fs';
import type * as Types from '../../../../@types';

export type Methods = DefineMethods<{
  put: {
    query?: {
      CodeAccessory?: string | undefined;
      Description?: string | null | undefined;
      Name?: string | null | undefined;
      Price?: number | undefined;
      Quantity?: number | undefined;
      Discount?: number | null | undefined;
      ShortDescription?: string | null | undefined;
      CategoryId?: string | null | undefined;
      ImageId?: string[] | null | undefined;
    } | undefined;

    status: 200;
    resBody: Types.MPSUpdateAccessoryResponse;
    reqFormat: FormData;

    reqBody: {
      Images?: (File | ReadStream)[] | null | undefined;
    };
  };
}>;
