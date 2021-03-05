terraform {
  required_providers {
    kubernetes = {
      source  = "hashicorp/kubernetes"
      version = ">= 2.0.0"
    }
  }
}

provider "kubernetes" {
  config_path = "~/.kube/config"
}

resource "kubernetes_namespace" "communications" {
  metadata {
    name = "communications-business"
  }
}

resource "kubernetes_deployment" "communications" {
  metadata {
    name = "communications-business"
    namespace = kubernetes_namespace.communications.metadata.0.name
    labels = {
      app = "CommunicationsBusiness"
    }
  }

  spec {
    replicas = 2
    selector {
      match_labels = {
        app = "communications-business"
      }
    }
    template {
      metadata {
        labels = {
          app = "communications-business"
        }
      }
      spec {
        container {
          image = "communications-business:latest"
          name = "communications-business-container"
          image_pull_policy = "IfNotPresent"
          port {
            container_port = 80
            protocol = "TCP"
          }
        }
      }
    }
  }
}