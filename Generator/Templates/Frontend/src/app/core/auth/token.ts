export class Token {
    // .expires: String; // "Wed, 10 May 2017 22:36:43 GMT"
    // .issued: String; // "Tue, 09 May 2017 22:36:43 GMT"
    expires: Date;
    access_token: String;
    expires_in: number;
    roles: Array<String>;
    token_type: String;
    userName: String;
}