-- Создание базы данных
CREATE DATABASE IF NOT EXISTS ecommerce_db;
USE ecommerce_db;

-- Таблица Категории
CREATE TABLE Categories (
    id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(255) NOT NULL
);

-- Таблица Пользователи
CREATE TABLE Users (
    id INT PRIMARY KEY AUTO_INCREMENT,
    login VARCHAR(50) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL,
    address TEXT,
    email VARCHAR(255) UNIQUE NOT NULL,
    phone_number VARCHAR(20),
    role ENUM('admin', 'customer', 'supplier') DEFAULT 'customer',
    name VARCHAR(255) NOT NULL
);

-- Таблица Промокоды
CREATE TABLE PromoCodes (
    id INT PRIMARY KEY AUTO_INCREMENT,
    code VARCHAR(50) UNIQUE NOT NULL,
    discount DECIMAL(5,2) NOT NULL,
    start_date DATETIME NOT NULL,
    end_date DATETIME NOT NULL
);

-- Таблица Продукты
CREATE TABLE Products (
    id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(255) NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    description TEXT,
    manufacturer VARCHAR(255),
    quantity INT DEFAULT 0,
    category_id INT,
    FOREIGN KEY (category_id) REFERENCES Categories(id) ON DELETE SET NULL
);

-- Таблица Корзины
CREATE TABLE Carts (
    id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT UNIQUE NOT NULL,
    created_date DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES Users(id) ON DELETE CASCADE
);

-- Таблица Элементы корзины
CREATE TABLE CartItems (
    id INT PRIMARY KEY AUTO_INCREMENT,
    cart_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL DEFAULT 1,
    FOREIGN KEY (cart_id) REFERENCES Carts(id) ON DELETE CASCADE,
    FOREIGN KEY (product_id) REFERENCES Products(id) ON DELETE CASCADE,
    UNIQUE KEY unique_cart_product (cart_id, product_id)
);

-- Таблица Заказы
CREATE TABLE Orders (
    id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT NOT NULL,
    status ENUM('pending', 'processing', 'shipped', 'delivered', 'cancelled') DEFAULT 'pending',
    created_date DATETIME DEFAULT CURRENT_TIMESTAMP,
    promo_code_id INT NULL,
    FOREIGN KEY (user_id) REFERENCES Users(id) ON DELETE CASCADE,
    FOREIGN KEY (promo_code_id) REFERENCES PromoCodes(id) ON DELETE SET NULL
);

-- Таблица Элементы заказа
CREATE TABLE OrderItems (
    id INT PRIMARY KEY AUTO_INCREMENT,
    order_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    FOREIGN KEY (order_id) REFERENCES Orders(id) ON DELETE CASCADE,
    FOREIGN KEY (product_id) REFERENCES Products(id) ON DELETE CASCADE
);

-- Создание индексов для улучшения производительности
CREATE INDEX idx_users_email ON Users(email);
CREATE INDEX idx_users_login ON Users(login);
CREATE INDEX idx_products_category ON Products(category_id);
CREATE INDEX idx_orders_user ON Orders(user_id);
CREATE INDEX idx_orders_status ON Orders(status);
CREATE INDEX idx_cart_items_cart ON CartItems(cart_id);
CREATE INDEX idx_order_items_order ON OrderItems(order_id);
CREATE INDEX idx_promo_codes_code ON PromoCodes(code);
CREATE INDEX idx_promo_codes_dates ON PromoCodes(start_date, end_date);

