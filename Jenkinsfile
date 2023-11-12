pipeline {
  agent any
  environment {
    DOCKER_IMAGE = "test"
  }
  stages {
    stage("Java build") {
      steps {
        sh '''
          cd ./springapp
          chmod +x gradlew
          ./gradlew build
          ls
          java -jar build/libs/your-application-name.jar
        '''
      }
    }
     stage("verify tooling") {
      steps {
        sh '''
          docker version
        '''
      }
    }
    

    
    // stage("verify tooling") {
    //   steps {
    //     sh '''
    //       docker version
    //       docker info
    //       docker-compose version 
    //       curl --version
    //       jq --version
    //     '''
    //   }
    // }
    // stage('Prune Docker data') {
    //   steps {
    //     sh 'docker system prune -a --volumes -f'
    //   }
    // }
    // stage('Start container') {
    //   steps {
    //     sh 'docker compose up -d --no-color --wait'
    //     sh 'docker compose ps'
    //   }
    // }
    // stage('Run tests against the container') {
    //   steps {
    //     sh 'curl http://localhost:3000 | jq'
    //   }
    // }
  }
  post {
    always {
      sh 'docker compose down --remove-orphans -v'
      sh 'docker compose ps'
    }
  }
}