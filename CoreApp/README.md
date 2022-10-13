Generating the Private Key -- Windows
openssl genrsa -des3 -out {ten_file.key} {do_dai_key}
openssl genrsa -des3 -out cert.key 2048
openssl genrsa -out rsa.private 1024

Generating the Public Key -- Windows
openssl rsa -in rsa.private -out rsa.public -pubout -outform PEM

Generating the Public Key -- Ubuntu
openssl req -x509 -new -nodes -key {ten_file.key} -sha256 -days 1024 -out {ten_file.pem}
openssl req -x509 -new -nodes -key cert.key -sha256 -days 1024 -out cert.pem

openssl genrsa -out cert.key 2048
openssl rsa -in cert.key -out cert.pem -pubout -outform PEM

openssl genrsa -out rsa.key 1024
openssl rsa -in rsa.key -out rsa.pem -pubout -outform PEM

openssl genrsa -passout pass:123456 -out private.pem 2048
openssl rsa -in private.pem -out public.pem -pubout -outform PEM
`