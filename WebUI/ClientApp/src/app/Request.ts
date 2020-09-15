import { RequestStatusEnum } from './RequestStatusEnum';

class BaseRequest {
  id: number;
  numberOfReviews: number;
  status: RequestStatusEnum;
  date: Date;

  constructor(numberOfReviews: number, date: Date, status: RequestStatusEnum = RequestStatusEnum.Ongoing) {
    this.numberOfReviews = numberOfReviews;
    this.date = date;
    this.status = status;
  } 

}

export class Request extends BaseRequest{
  productId: string;

  constructor(productId: string, numberOfReviews: number, date: Date) {
    super(numberOfReviews, date);
    this.productId = productId;
  } 
}

export class BulkRequest extends BaseRequest{
  productIds: string[];
}
