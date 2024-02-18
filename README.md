# Documentation: 
[https://digitaliser.dk/Media/638336537871668450/Digital%20Post%20-%20Technical%20Integration%20v1.41.pdf](https://digitaliser.dk/Media/638416000215044812/Digital%20Post%20-%20Technical%20Integration%20v1.43.pdf)

https://digitaliser.dk/digital-post/vejledninger/technical-integration

https://digst.dk/it-loesninger/digital-post/vejledninger/

# Administration
https://admin.digitalpost.dk/ 
 
# APIs
Test 
[https://test.digitalpost.dk/api](https://test.digitalpost.dk/api)

Prod
[https://api.digitalpost.dk/](https://api.digitalpost.dk/apis/v1)


# Source Code
Reference implementation: 
https://bitbucket.org/nc-dp/reference-systems-for-dotnet/src/master/

Memolib:
https://bitbucket.org/nc-dp/memo-lib-dot-net/src/master/ 


# Authentication
Authorization header is for identifiying calling system, the actual auth is the mutual tls. 

curl -v \
--http1.1 \
--key "my_foces_certificate.pkcs8" \
--key-type pem \
--cert-type pem \
--cert "my_foces_certificate.cer" \
-H "Authorization: Basic
ZTU0NWIzMzktODU0Mi00YTMwLWFlNzUtYzY3ZTRkMmE3Yjk5OjE1NjMyYThkLTQwN2MtNGMzYS1iN2IxLWFlY
mFhNTE0ZmNhYg==" \
"https://api.test.digitalpost.dk/apis/v1/contacts/"


# Getting a certificate: 
https://erhvervsadministration.nemlog-in.dk/certificates 

## Converting pkcs12 to pem 

Extract cert and private key
```
openssl pkcs12 -in path.p12 -out newfile.crt.pem -clcerts -nokeys
openssl pkcs12 -in path.p12 -out newfile.key.pem -nocerts -nodes
```
Combine into one pem (no pass)
```
openssl pkcs12 -in path.p12 -out newfile.pem -nodes
```


# Calling prod

curl -v \
--http1.1 \
--key "SJKPConsulting.key.pem" \
--key-type pem \
--cert-type pem \
--cert "SJKPConsulting.crt.pem" \
-H "Authorization: Basic OGVjNWMyNjEtMWZlYi00ZmIyLWJmMWItYTdmNjIxZmFmN2M2OjUxMTYwOGE1LTE4ZjQtNDFmYS1iNmM1LWNjOTM4NmUyN2MzMA==" \
"https://api.digitalpost.dk/apis/v1/contacts/"


Result
```json
{
  "currentPage" : 0,
  "next" : "WyAxNjkyOTI0NTc0NDM3LCAiRmlOSFhhZ0xRUXNaTHl6Y0Y0T2szdHlkSkhCb1lrVHciIF0=",
  "totalPages" : 1,
  "elementsOnPage" : 1,
  "totalElements" : 1,
  "contacts" : [ {
    "id" : "62e55094-66d3-4247-822b-ed57d416819b",
    "version" : 3,
    "type" : "COMPANY",
    "transactionId" : "FiNHXagLQQsZLyzcF4Ok3tydJHBoYkTw",
    "cvrNumber" : "34485771",
    "mailboxSubscription" : {
      "id" : "d1772f49-d624-480a-bd7e-7e5957397e99",
      "version" : 1,
      "registrationStatus" : "AUTOMATIC_REGISTRATION",
      "publicRegistrationStatus" : "REGISTERED",
      "startTime" : "2013-11-21T00:08:06.770Z"
    },
    "lastUpdated" : "2023-08-25T00:49:34.437Z",
    "createdDate" : "2012-05-22T11:55:01.832Z",
    "eligibleForVoluntaryRegistration" : false,
    "address" : {
      "@type" : "CompanyAddress",
      "id" : "1ff804dc-1365-4fcd-92a6-beacd19a7f88",
      "version" : 0,
      "streetName" : "Hovedvejen",
      "streetCode" : "437",
      "postalCode" : "8670",
      "municipalityName" : "SKANDERBORG",
      "municipalityCode" : "746",
      "postDistrict" : "Låsby",
      "houseNumberFrom" : "17",
      "careOfName" : "Flensted Byggeteknik"
    }
  } ]
}
``` 
