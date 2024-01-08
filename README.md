## 検証
#### GETコントローラー
デバッグ実行後、ローカルホストで`/sample`に`GETリクエスト`で、
`param1 is null or white space`を返却

ソースコードとしては、`[HttpGet(Name = "sample")]`で処理

#### リクエストパラメータ
`/sample?param1=文言`でリクエストで、`request success param1 = 文言`を返却

ソースコードとしては、`String GETメソッド([FromQuery(Name = "param1")] string? param1)`で処理
（`Name`は短縮可能）
