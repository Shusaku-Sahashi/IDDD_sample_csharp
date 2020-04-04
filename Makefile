JDK_CONTENA_NAME=iddd-sample 
CONTENA_NETWORK_NAME=iddd-samples-network 
host=127.0.0.1
mysqlUser=root

.PHONY: init-env
init-env:
	docker-compose up -d

.PHONY: init-db
init-db:
	$(eval testSqlFiles := $(shell find ${PWD} -name *.sql | grep -i test))
	$(eval sqlFiles := $(shell find ${PWD} -name *.sql | grep -vi test))

	@for sql in ${testsqlfiles}; do \
		echo "importing [$$sql]"; \
		mysql --host="${host}" --port=3306 --protocol=tcp --user="${mysqluser}" < $$sql; \
	done
	@for sql in ${sqlfiles}; do \
		echo "importing [$$sql]"; \
		mysql --host="${host}" --port=3306 --protocol=tcp --user="${mysqluser}" < $$sql; \
	done

.PHONY: init-java
init-java:
	docker run -d -it -v ${PWD}:/tmp/iddd-sample -p 8080:8080  --network ${CONTENA_NETWORK_NAME} --name ${JDK_CONTENA_NAME} openjdk:7 /bin/sh

.PHONY: start
start: init-env init-db init-java

.PHONY: stop
stop:
	docker-compose stop
	docker stop ${JDK_CONTENA_NAME}